// Большаков А.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;

using Tao.FreeGlut;


namespace Lab_work_4
{
    public partial class MainWindow : Window
    {
        public double rotation;
        public double RotationAct2;
        public double Event1;
        public double Event2;
        public double ControlIter;
        public double Translate1 = 0;
        private double Count = 29;
        private double Angle = 0;
        private double Temp;
        private double Temp1;
        private double iter1 = 1;
        private double iter2 = 1;
        private int control = 0;
        private double Key1 = 0;
        private double Key2 = 0;
        private double MainKey1 = 0;
        private double MainKey2 = 0;
        private double scale = 0;
        private double position = 0;
        private int CounterCube = 0;
        private double Speed = 1;
        private double[,] ColorArray1 = new double[100, 3];


        public MainWindow()
        {
            InitializeComponent();
            rotation = 0.0f;
        }


        private void openGLControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);   // установка режима отображения

            gl.ClearColor(0.0f, 0.02f, 0.05f, 0f);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            float[] pos = new float[4] { -100.0f, 1.0f, -19.0f, 0.9f };
            float[] pos1 = new float[3] { -1.0f, -1.0f, -1.0f };
            float[] material = new float[4] { 0.0f, 0.1f, 0.3f, 1.0f };
            float[] global_ambient = new float[4] { 0f, 0.3f, 0.3f, 1.0f };

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, pos1);

            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, material);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, 128);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(70, (float)Width / (float)Height, 0.01, 100.0);
        }


        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.LookAt(-2, 0, 0, 0, 0, 0, 0, 1, 0);

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();

            gl.LoadIdentity();

            gl.Translate(scale, position, 0);

            gl.Rotate(Angle, 0.0f, 0.0f, 2.0f);
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            gl.Color(0.5, 0.5, 0.6);
            Set(gl, 0, 0, 0, 0.3);

            Set(gl, 0, -0.1, -0.2, 0.1);
            Set(gl, 0, 0.1, 0, 0.1);

            Set(gl, 0, -0.1, 0.4, 0.1);
            Set(gl, 0, 0.1, 0, 0.1);

            Set(gl, -0.2, -0.1, -0.2, 0.1);
            Set(gl, 0, 0.1, 0, 0.1);

            Set(gl, 0.4, -0.1, 0, 0.1);
            Set(gl, 0, 0.1, 0, 0.1);

            Set(gl, -0.2, 0.2, 0, 0.1);

            if (Key1 == 0)
            {
                gl.Color(0.8, 0.4, 0.8);
                Set(gl, 0, 0.1, 0, 0.05);
                gl.Color(0.9, 0.3, 0.2);
                Set(gl, 0.2, -0.2, 0, 0.05);
                gl.Color(0.2, 0.9, 0.3);
                Set(gl, -0.2, 0, 0.2, 0.05);
                gl.Color(0.3, 0.2, 0.9);
                Set(gl, 0, 0, -0.4, 0.05);
                gl.Color(0.8, 0.7, 0.2);
                Set(gl, -0.2, 0, 0.2, 0.05);
                gl.Translate(0.2, 0.2, 0);
            }
            else
                gl.Translate(0, 0.1, 0);

            if (Count >= 1)
            {
                if (control == 1)
                { 
                    control = 0;
                    Temp = 1;
                    iter1 = 1;
                    iter2 = 1;
                    Key1 = 0;
                    MainKey1 = 0;
                    MainKey2 = 0;

                    Random random = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        ColorArray1[i, 0] = (int)random.Next(255, 705);
                        ColorArray1[i, 1] = (int)random.Next(255, 705);
                        ColorArray1[i, 2] = (int)random.Next(255, 705);
                    }
                }

                if (31 == iter1)
                {
                    iter1 = 1;
                    MainKey1 = 1;
                    MainKey2 = 0;
                    Temp = 1;
                }
                else if (Count <= (int)iter2)
                {
                    iter2 = 1;
                    MainKey2 = 1;
                    MainKey1 = 0;
                    Temp = 1;
                }

                if (MainKey1 == 0)
                {
                    gl.Translate(-0.2, -0.2, 0);
                    Act_1(gl, (int)Temp, iter1);
                    if ((int)Temp < 31) Temp += 1;
                }
                else if (MainKey2 == 0 && MainKey1 == 1)
                {
                    gl.Translate(0, (0.025 * 28) - 0.2, 0);
                    Act_2(gl, (int)Temp, (int)iter2);
                    if ((int)Temp < Count) Temp += 1;
                }
            }

            gl.PopMatrix();
            gl.Flush();

            rotation += Speed;
        }


        private int Set(OpenGL gl, double parameter1, double parameter2, double parameter3, double SizeCube)
        {
            gl.Translate(parameter1, parameter2, parameter3);
            Glut.glutSolidCube(SizeCube);
            return 0;
        }


        private int Cube(OpenGL gl, double parameter1, double parameter2)
        {
            CounterCube += 1;
            if (CounterCube >= 100) CounterCube = 0;
            gl.Translate(parameter1, 0, parameter2);
            gl.Color((byte)ColorArray1[CounterCube, 0], (byte)ColorArray1[CounterCube, 1], (byte)ColorArray1[CounterCube, 2]);
            Glut.glutSolidCube(0.05);
            return 0;
        }


        private int Act_1(OpenGL gl, double Counter, double iteration)
        {
            gl.Translate(0, 0.025 * iteration, 0);

            if (iteration >= 1 && iteration <= 12)
            {
                Event1 = 0.025 * iteration;
            }
            else if (iteration >= 13 && iteration <= 31)
            {
                Temp1 = 12 - (iteration - 12);
                Event1 = 0.025 * Temp1;
            }

            if (iteration >= 6 && iteration <= 16)
            {
                gl.Translate(0.2, 0.2 + 0.025 * (iteration - 10), 0);
                Cube(gl, 0, 0);
                gl.Translate(-0.2, -0.2 - 0.025 * (iteration - 10), 0);
            }

            Cube(gl, -1 * Event1, 0);
            gl.Translate(0.4 + Event1, 0, 0);

            Cube(gl, Event1, 0);
            gl.Translate(-0.2 - 1 * Event1, 0, 0.2);

            Cube(gl, 0, Event1);
            gl.Translate(0, 0, -0.4 - 1 * Event1);

            Cube(gl, 0, -1 * Event1);
            gl.Translate(-0.2, -0.025 * iteration, 0.2 + Event1);


            iteration += 1;

            if (31 == iteration)
            {
                Key1 = 1;
                iter1 += 1;
            }

            if (Counter > iteration)
                return Act_1(gl, Counter, iteration);
            else
                return 0;
        }


        private int Act_2(OpenGL gl, double Counter, double iteration)
        {
            ControlIter = (int)(iteration / 5);

            Event2 = 0.05 * iteration;
            if (ControlIter > 0)
                    Event2 = 0.05 * (iteration - (5 * ControlIter));
            
            gl.Translate(0, 0.05 * iteration, 0);

            if (iteration == 5 * ControlIter)
                Translate1 = 0.05 * ((int)Temp - iteration);
            else
                Translate1 = 0.05 * iteration;

            if (iteration <= 1)
            {
                if (iteration >= 6 && iteration <= 16)
                {
                    gl.Translate(0.2, 0.2 + 0.025 * (iteration - 10), 0);
                    Cube(gl, 0, 0);
                    gl.Translate(-0.2, -0.2 - 0.025 * (iteration - 10), 0);
                }

                gl.Translate(-1 * Translate1, 0, 0);
                Cube(gl, -1 * Event2 , 0);

                gl.Translate(2 * Translate1, 0, 0);
                Cube(gl, Event2 * 2, 0);

                gl.Translate(-1  * Translate1, 0, Translate1);
                Cube(gl, -1 * Event2, Event2);

                gl.Translate(0, 0, -2 * Translate1);
                Cube(gl, 0, -2 * Event2);

                gl.Translate(0, 0, Translate1);
                gl.Translate(0, 0, Event2);
            }
            else
            {
                if (iteration >= 7 && iteration <= Count-10)
                {
                    gl.Translate(0, 0.2 + 0.05 * (iteration - 10), 0);
                    Cube(gl, 0, 0);
                    gl.Translate(0, -0.2 - 0.05 * (iteration - 10), 0);
                }

                gl.Translate(-1 * Translate1, 0, Translate1);
                Cube(gl, -1 * Event2, Event2 - 0.05);

                gl.Translate(2 * Translate1, 0, -2 * Translate1);
                Cube(gl, Event2 * 2, -2 * Event2 + 0.1);

                gl.Translate(0, 0, 2 * Translate1);
                Cube(gl, -0.05, 2 * Event2 - 0.05);

                gl.Translate(-2 * Translate1, 0, -2 * Translate1);
                Cube(gl, -2 * Event2 + 0.1, -2 * Event2);

                gl.Translate(Translate1, 0, Translate1);
                gl.Translate(Event2  - 0.05, 0.2, Event2);



                gl.Translate(-1 * Translate1, 0, Translate1);
                Cube(gl, -1 * Event2, Event2 - 0.05);

                gl.Translate(2 * Translate1, 0, -2 * Translate1);
                Cube(gl, Event2 * 2, -2 * Event2 + 0.1);

                gl.Translate(0, 0.2, 2 * Translate1);
                Cube(gl, -0.05, 2 * Event2 - 0.05);

                gl.Translate(-2 * Translate1, 0, -2 * Translate1);
                Cube(gl, -2 * Event2 + 0.1, -2 * Event2);

                gl.Translate(Translate1, 0, Translate1);
                gl.Translate(Event2 - 0.05, -0.2, Event2);
            }

            gl.Translate(0, -0.05 * iteration, 0);

            iteration += 1;

            if (Count == iteration)
            {
                if (Count >= 1 && Count <= 40)
                    iter2 += 1;
                else
                    iter2 += 2;
            }

            if (Counter >= iteration)
                return Act_2(gl, Counter, iteration);
            else
                return 0;
        }


        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
        }

        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {}

        private void openGLControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.Viewport(1, 1, (int)Width, (int)Height);
            gl.LoadIdentity();
            gl.Perspective(37.0f, (double)Width / (double)Height, 2, 4.0);
            gl.Flush();
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            Count = Convert.ToDouble(Count1.Text);
            control = 1;
        }

        private void Angle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Angle = ((double)e.NewValue);
        }

        private void Speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Speed = ((double)e.NewValue);
        }

        private void Scaling_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scale = ((double)e.NewValue)/10;
        }

        private void Position_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            position = ((double)e.NewValue) / 25;
        }

    }
}