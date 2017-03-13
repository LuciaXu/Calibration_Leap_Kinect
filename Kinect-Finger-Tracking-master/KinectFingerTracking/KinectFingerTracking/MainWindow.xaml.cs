using LightBuzz.Vitruvius.FingerTracking;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;
using Leap;
using System;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;

namespace KinectFingerTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool frame_freeze = false;
        private bool calib_finished = false;
        private bool need_calib = true;
        private Matrix<float> R = new Matrix<float>(3, 3);
        private Matrix<float> T = new Matrix<float>(3, 1);

        List<CameraSpacePoint> k_list = new List<CameraSpacePoint>();
        List<Matrix<float>> l_list = new List<Matrix<float>>();
        //above is added by lu for calibration
        private KinectSensor _sensor = null;
        private InfraredFrameReader _infraredReader = null;
        private DepthFrameReader _depthReader = null;
        private BodyFrameReader _bodyReader = null;
        private IList<Body> _bodies;
        private Body _body;

        // Create a new reference of a HandsController.
        private HandsController _handsController = null;
        private Adjust adjust = new Adjust();

        /// <summary>
        /// The main window of the app.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            adjust.my_ok += new Adjust.OK_Dele(OK_event);
            if(need_calib)
            {
                calib_finished = false;
                string path = "F:\\calib_data\\calib_data.txt";
                bool FileValid = System.IO.File.Exists(path);
                if(FileValid)
                {
                    using (System.IO.TextReader file = new System.IO.StreamReader(@"F:\\calib_data\\calib_data.txt"))
                    {
                        string linestring;
                        while ((linestring = file.ReadLine()) != null)
                        {
                            CameraSpacePoint kinect_data = new CameraSpacePoint();
                            string[] arr = linestring.Split(' ');
                            kinect_data.X = float.Parse(arr[0]);
                            kinect_data.Y = float.Parse(arr[1]);
                            kinect_data.Z = float.Parse(arr[2]);
                            k_list.Add(kinect_data);

                            Matrix<float> leap_data = new Matrix<float>(3, 1);
                            leap_data[0, 0] = float.Parse(arr[3]);
                            leap_data[1, 0] = float.Parse(arr[4]);
                            leap_data[2, 0] = float.Parse(arr[5]);
                            l_list.Add(leap_data);

                        }

                    }
                }
                R.SetZero();
                T.SetZero();
            }
            else
            {
                calib_finished = true;
                using (System.IO.TextReader file = new System.IO.StreamReader(@"F:\\calib_data\\calib_matrix.txt"))
                {
                    string linestring;
                    linestring = file.ReadLine();
                    R[0, 0] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[0, 1] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[0, 2] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[1, 0] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[1, 1] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[1, 2] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[2, 0] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[2, 1] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    R[2, 2] = float.Parse(linestring);

                    linestring = file.ReadLine();
                    T[0, 0] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    T[1, 0] = float.Parse(linestring);
                    linestring = file.ReadLine();
                    T[2, 0] = float.Parse(linestring);

                }
            }
            

               

            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _depthReader = _sensor.DepthFrameSource.OpenReader();
                _depthReader.FrameArrived += DepthReader_FrameArrived;

                _infraredReader = _sensor.InfraredFrameSource.OpenReader();
                _infraredReader.FrameArrived += InfraredReader_FrameArrived;

                _bodyReader = _sensor.BodyFrameSource.OpenReader();
                _bodyReader.FrameArrived += BodyReader_FrameArrived;
                _bodies = new Body[_sensor.BodyFrameSource.BodyCount];

                // Initialize the HandsController and subscribe to the HandsDetected event.
                _handsController = new HandsController();
                _handsController.HandsDetected += HandsController_HandsDetected;

                _sensor.Open();
            }

        }

        private void OK_event(int r_x, int r_y)
        {

        }

        private void DepthReader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            
            if(!frame_freeze)
            {
            canvas.Children.Clear();

             using (DepthFrame frame = e.FrameReference.AcquireFrame())
             {
                if (frame != null)
                {
                    // 2) Update the HandsController using the array (or pointer) of the depth depth data, and the tracked body.
                    using (KinectBuffer buffer = frame.LockImageBuffer())
                    {
                        _handsController.Update(buffer.UnderlyingBuffer, _body);
                    }
                }
             }
            }
                

        }

        private void InfraredReader_FrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            if(!frame_freeze)
            {
                using (var frame = e.FrameReference.AcquireFrame())
                {
                    if ((frame != null))
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

        }

        private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            if(!frame_freeze)
            {
                using (var bodyFrame = e.FrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
                        bodyFrame.GetAndRefreshBodyData(_bodies);

                        _body = _bodies.Where(b => b.IsTracked).FirstOrDefault();
                    }
                }
            }

        }

        private void HandsController_HandsDetected(object sender, HandCollection e)
        {
            // Display the results!
            if (e.HandRight != null)
            {
                // Draw contour.
                foreach (var point in e.HandRight.ContourDepth)
                {
                    DrawEllipse(point, Brushes.Blue, 2.0);
                }
                // Draw fingers.
                foreach (var finger in e.HandRight.Fingers)
                {
                    DrawEllipse(finger.DepthPoint, Brushes.White, 4.0);
                }

                //added by lu for calib
                if(!calib_finished)
                {
                    if(e.HandRight.Fingers.Count==5)// if there are five fingers than than do calibration
                    {
                        //do_calib
                        //first get fingers from leap
                        Controller controller = new Controller();
                        Leap.Frame leapframe = controller.Frame();
                        

                        if (leapframe.Fingers.Count == 5)
                        {
                            if (MessageBox.Show("Is the data qualified?","Calibration",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No)==MessageBoxResult.Yes)
                            {   
                                //then sort the fingers of kinect
                                Sort_Kinect_Points(e.HandRight.Fingers);
                                for (int i = 0; i < 5; i++)
                                {
                                    Matrix<float> leapfinger_m = new Matrix<float>(3, 1);
                                    leapfinger_m[0, 0] = leapframe.Fingers[i].StabilizedTipPosition.x;
                                    leapfinger_m[1, 0] = leapframe.Fingers[i].StabilizedTipPosition.y;
                                    leapfinger_m[2, 0] = leapframe.Fingers[i].StabilizedTipPosition.z;
                                    l_list.Add(leapfinger_m);
                                }
                                int k_list_num = k_list.Count();
                                int l_list_num = l_list.Count();
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"F:\\calib_data\\calib_data.txt",true))
                                {
                                    for(int write_num =5; write_num>0;write_num--)
                                    {
                                    
                                        file.Write(k_list[k_list_num - write_num].X+" ");
                                        file.Write(k_list[k_list_num - write_num].Y+" ");
                                        file.Write(k_list[k_list_num - write_num].Z+" ");
                                        file.Write(l_list[l_list_num - write_num][0, 0]+" ");
                                        file.Write(l_list[l_list_num - write_num][1, 0] + " ");
                                        file.Write(l_list[l_list_num - write_num][2, 0] + "\r\n");
                                    }
                                }
                                //calibration
                                bool calib_done = do_calib(k_list, l_list);
                                if (calib_done)
                                {
                                    
                                    List<Matrix<float>> points_test = transform_points(leapframe.Fingers,leapframe.Hand(0));
                                    for (int pointn = 0; pointn < points_test.Count(); pointn++)
                                    { 
                                        DrawEllipse(points_test[pointn], Brushes.Red, 5.0);
                                    }

                                   
                                    if (MessageBox.Show("Is calibration finished?", "Calibration", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                                    {
                                        calib_finished = true;
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"F:\\calib_data\\calib_matrix.txt", true))
                                        {
                                            file.WriteLine(R[0, 0]);
                                            file.WriteLine(R[0, 1]);
                                            file.WriteLine(R[0, 2]);
                                            file.WriteLine(R[1, 0]);
                                            file.WriteLine(R[1, 1]);
                                            file.WriteLine(R[1, 2]);
                                            file.WriteLine(R[2, 0]);
                                            file.WriteLine(R[2, 1]);
                                            file.WriteLine(R[2, 2]);
                                            file.WriteLine(T[0, 0]);
                                            file.WriteLine(T[1, 0]);
                                            file.WriteLine(T[2, 0]);
                                        }
                                    }
                                    else
                                    {
                                        calib_finished = false;
                                    }
                                }
                            }
                            else
                            {
                                return; }

                        }
                    }

                }
                else
                {
                    frame_freeze = true;
                    //draw_leap
                    Controller controller = new Controller();
                    Leap.Frame leapframe = controller.Frame();
                    if(leapframe.Fingers.Count()!=0)
                    {
                        Leap.Hand leap_hand = leapframe.Hands.First();
                        List<Matrix<float>> points_kl = transform_points(leapframe.Fingers, leap_hand);
                        for (int pointn = 0; pointn < 5; pointn++)
                        {
                            DrawEllipse(points_kl[pointn], Brushes.Red, 5.0);// 0 ~ 4 fingertips
                        }
                        DrawEllipse(points_kl[5], Brushes.BlueViolet, 7.0);//palm position
                        for (int pointn = 6; pointn < points_kl.Count();pointn++ )
                        {
                            DrawEllipse(points_kl[pointn], Brushes.Yellow, 3.0);// finger bones
                        }

                        //MessageBox.Show("projection done!");
                        if (MessageBox.Show("Start adjusting?", "Projection done!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                        {
                            
                            adjust.Show();
                            return;
                        }
                        else
                        {
                            
                        }
                    }
                    frame_freeze = false;
                }
            }
        }

        private void DrawEllipse(Matrix<float> new_leap, Brush brush, double radius)
        {
            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = radius,
                Height = radius,
                Fill = brush
            };

            canvas.Children.Add(ellipse);
            CameraSpacePoint camera_point = new CameraSpacePoint();
            camera_point.X = new_leap.Data[0, 0];
            camera_point.Y = new_leap.Data[1, 0];
            camera_point.Z = new_leap.Data[2, 0];
            DepthSpacePoint depth_point = _sensor.CoordinateMapper.MapCameraPointToDepthSpace(camera_point);
            depth_point.X = float.IsInfinity(depth_point.X) ? 0 : depth_point.X;
            depth_point.Y = float.IsInfinity(depth_point.Y) ? 0 : depth_point.Y;

            Canvas.SetLeft(ellipse, depth_point.X - radius / 2.0);
            Canvas.SetTop(ellipse, depth_point.Y - radius / 2.0);
        }

        List<Matrix<float>> transform_points(FingerList leap_points, Leap.Hand hand)
        {
            int pointsnum = leap_points.Count();
            List<Matrix<float>> new_leap = new List<Matrix<float>>();
            for(int i =0; i<pointsnum;i++)
            {
                Matrix<float> leap_point = new Matrix<float>(3,1);
                //leap_point.Data[0, 0] = leap_points[i].StabilizedTipPosition.x/1000;
                //leap_point.Data[1, 0] = leap_points[i].StabilizedTipPosition.y/1000;
                //leap_point.Data[2, 0] = leap_points[i].StabilizedTipPosition.z/1000;
                leap_point.Data[0, 0] = leap_points[i].TipPosition.x / 1000;
                leap_point.Data[1, 0] = leap_points[i].TipPosition.y / 1000;
                leap_point.Data[2, 0] = leap_points[i].TipPosition.z / 1000;
                Matrix<float> trans_points = new Matrix<float>(3, 1);
                trans_points = R * leap_point + T;
                new_leap.Add(trans_points);//5 points 
            }

            Matrix<float> leap_palm = new Matrix<float>(3, 1);
            leap_palm.Data[0, 0] = hand.PalmPosition.x / 1000;
            leap_palm.Data[1, 0] = hand.PalmPosition.z / 1000;
            leap_palm.Data[2, 0] = hand.PalmPosition.y / 1000;

            Matrix<float> trans_palm = new Matrix<float>(3, 1);
            trans_palm = R * leap_palm + T;
            new_leap.Add(trans_palm);// 1 points

            for (int i = 0; i < pointsnum;i++ )
            {
                Bone bone;
                foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
                {
                   Matrix<float> leap_point = new Matrix<float>(3, 1);
                   bone = hand.Fingers[i].Bone(boneType);
                   leap_point.Data[0,0] = bone.PrevJoint.x/1000;
                   leap_point.Data[1,0] = bone.PrevJoint.y/1000;
                   leap_point.Data[2,0] = bone.PrevJoint.z/1000;
                   Matrix<float> trans_points = new Matrix<float>(3, 1);
                   trans_points = R * leap_point + T;
                   new_leap.Add(trans_points);// 4 * 5 = 20 points
                }
            }
                return new_leap;
        }



        private void Sort_Kinect_Points(IList<LightBuzz.Vitruvius.FingerTracking.Finger> kinectfingers)
        {
            var orderedlist = kinectfingers.OrderBy(fingertemp => fingertemp.CameraPoint.X).ToList();
            k_list.Add(orderedlist[0].CameraPoint);
            k_list.Add(orderedlist[1].CameraPoint);
            k_list.Add(orderedlist[2].CameraPoint);
            k_list.Add(orderedlist[3].CameraPoint);
            k_list.Add(orderedlist[4].CameraPoint);
        }

        private bool do_calib(List<CameraSpacePoint> k_list, List<Matrix<float>> l_list)
        {
            Matrix<float> l_mean = new Matrix<float>(3, 1);
            Matrix<float> k_mean = new Matrix<float>(3, 1);
            int k_num = k_list.Count();
            int l_num = l_list.Count();
            if (k_num != l_num) { MessageBox.Show("Number is not right!"); }
            for (int i = 0; i < k_num; i++)
            {
                l_mean.Data[0, 0] += l_list[i][0, 0] / 1000;
                l_mean.Data[1, 0] += l_list[i][1, 0] / 1000;
                l_mean.Data[2, 0] += l_list[i][2, 0] / 1000;

                k_mean.Data[0, 0] += k_list[i].X;
                k_mean.Data[1, 0] += k_list[i].Y;
                k_mean.Data[2, 0] += k_list[i].Z;
            }
            l_mean = (1.0 / k_num) * l_mean;
            k_mean = (1.0 / k_num) * k_mean;

            List<Matrix<float>> norm_l_data = new List<Matrix<float>>();
            List<Matrix<float>> norm_k_data = new List<Matrix<float>>();

            for (int i = 0; i < k_num; i++)
            {
                Matrix<float> ltemp = new Matrix<float>(3, 1);
                ltemp.Data[0, 0] = l_list[i][0, 0] - l_mean.Data[0, 0];
                ltemp.Data[1, 0] = l_list[i][1, 0] - l_mean.Data[1, 0];
                ltemp.Data[2, 0] = l_list[i][2, 0] - l_mean.Data[2, 0];
                norm_l_data.Add(ltemp);
                Matrix<float> ktemp = new Matrix<float>(3, 1);
                ktemp.Data[0, 0] = k_list[i].X - k_mean.Data[0, 0];
                ktemp.Data[1, 0] = k_list[i].Y - k_mean.Data[1, 0];
                ktemp.Data[2, 0] = k_list[i].Z - k_mean.Data[2, 0];
                norm_k_data.Add(ktemp);
            }

            Matrix<float> M = new Matrix<float>(3, 3);
            for (int i = 0; i < k_num; i++)
            {
                M = M + norm_l_data[i] * norm_k_data[i].Transpose();
            }

            Matrix<float> eigen_val = new Matrix<float>(3, 1);
            Matrix<float> eigen_vec = new Matrix<float>(3, 3);
            Matrix<float> eigen_vec_inv = new Matrix<float>(3, 3);
            Matrix<float> S = new Matrix<float>(3, 3);

            Matrix<float> M_M = M.Transpose() * M;
            CvInvoke.Eigen(M_M, eigen_val, eigen_vec);

            eigen_vec = eigen_vec.Transpose();

            for(int i =0; i<3;i++)
            {
                S.Data[i, i] = (float)Math.Sqrt(eigen_val.Data[i, 0]);
            }

            CvInvoke.Invert(eigen_vec, eigen_vec_inv, DecompMethod.LU);
            Matrix<float> M_M_sqrt = eigen_vec * S * eigen_vec_inv;

            Matrix<float> M_M_sqrt_inv = new Matrix<float>(3, 3);
            CvInvoke.Invert(M_M_sqrt, M_M_sqrt_inv, DecompMethod.Cholesky);

            
            R = M * M_M_sqrt_inv;
            T = k_mean - R * l_mean;


            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bodyReader != null)
            {
                _bodyReader.Dispose();
                _bodyReader = null;
            }

            if (_depthReader != null)
            {
                _depthReader.Dispose();
                _depthReader = null;
            }

            if (_infraredReader != null)
            {
                _infraredReader.Dispose();
                _infraredReader = null;
            }

            if (_sensor != null)
            {
                _sensor.Close();
                _sensor = null;
            }
        }

        private void DrawEllipse(DepthSpacePoint point, Brush brush, double radius)
        {
            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = radius,
                Height = radius,
                Fill = brush
            };

            canvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, point.X - radius / 2.0);
            Canvas.SetTop(ellipse, point.Y - radius / 2.0);
        }
    }
}
