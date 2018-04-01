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
        public double H1, H2;
        public double Res;
        public double G;
        private double Count = 0;
        private double Angle = 0;
        private double Temp = 1;
        private int control = 0;
        private int Yes = 0;
        private double Temp1;
        private double Temp2;
        private double Temp3;
        private double Temp4;
        private double iter;
        private double Key;
        private double scale = 0;
        private double position = 0;
        private double Speed = 1;
        private int CounterCube;
        private int CounterX = 1;
        private int[] ColorArray1;
        private int[] ColorArray2;
        private int[] ColorArray3;


        public MainWindow()
        {
            InitializeComponent();
            rotation = 0.0f;
            iter = 0;
        }


        private void openGLControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            Glut.glutInit();    // Инициализация Glut
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);   // установка режима отображения

            gl.ClearColor(0.6f, 0.7f, 0.9f, 0f);

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

            if (Count > 0)
            {
                if (control == 1)
                { 
                    control = 0;
                    Temp = 0;
                    Temp1 = 0;
                    Temp2 = 0;
                    Temp3 = 0;
                    Temp4 = 0;
                    H1 = 0;
                    H2 = 0;
                    Key = 0;
                    for (int i = 0; i < Count; i++)
                        CounterX = CounterX + (i * 8);

                    Array.Resize(ref ColorArray1, CounterX);
                    Array.Resize(ref ColorArray2, CounterX);
                    Array.Resize(ref ColorArray3, CounterX);

                    Random random = new Random();
                    for (int i = 0; i < CounterX; i++)
                    {
                        ColorArray1[i] = (int)random.Next(255, 705);
                        ColorArray2[i] = (int)random.Next(255, 705);
                        ColorArray3[i] = (int)random.Next(255, 705);
                    }
                }

                if (Temp1 == H1 && Temp2 == H2 && Temp3 == H1 && Temp4 == H2 && Count > Temp)
                { Temp1 = 0; Temp2 = 0; Temp3 = 0; Temp4 = 0; Temp += 1; H1 = 0; H2 = 0; }

                Function(gl, (int)Temp, iter);
            }


            gl.PopMatrix();
            gl.Flush();

            rotation += Speed;
        }


        private int Cube(OpenGL gl, double CounterCubes, double iteration, double Limit, double parameter1, double parameter2, double parameter3)
        {
            for (int j = 0; j < CounterCubes; j++)
            {

                if (Limit > j) Yes = 1;
                else if (iteration < (int)Temp - 1) Yes = 1; 

                if (Yes == 1)
                {
                    CounterCube += 1;
                    Yes = 0;
                    gl.Translate(parameter1, parameter2, parameter3);
                    gl.Color((byte)ColorArray1[CounterCube], (byte)ColorArray2[CounterCube], (byte)ColorArray3[CounterCube]);
                    Glut.glutSolidCube(0.1);
                }
            }
            return 0;
        }


        private int Function(OpenGL gl, double Counter, double iteration)
        {
            

            gl.Translate(0, 0, 0);

            if (iteration == 0)
            {
                CounterCube = 0;
                gl.Color((byte)ColorArray1[0], (byte)ColorArray2[0], (byte)ColorArray3[0]);
                Glut.glutSolidCube(0.1);
                gl.Translate(0, 0, -0.1);
            }
            else
            {
                G = iteration * 8;
                H1 = (G / 4) + 1;
                H2 = (G / 4) - 1;

                gl.Translate(0.2, -0.1, 0.2);
                Cube(gl, H1, iteration, Temp1, -0.1, 0, 0);
                if (Temp1 < H1) Temp1 += 1;

                Cube(gl, H2, iteration, Temp2, 0, 0, -0.1);
                if (Temp2 < H2 && Temp1 == H1) Temp2 += 1;

                gl.Translate(-0.1, 0, -0.1);
                Cube(gl, H1, iteration, Temp3, 0.1, 0, 0);
                if (Temp3 < H1 && Temp1 == H1 && Temp2 == H2) Temp3 += 1;

                Cube(gl, H2, iteration, Temp4, 0, 0, 0.1);
                if (Temp4 < H2 && Temp1 == H1 && Temp2 == H2 && Temp3 == H1) Temp4 += 1;
            }

            iteration += 1;

            if (Counter > iteration)
            {
                if (Temp1 == H1 && Temp2 == H2 && Temp3 == H1 && Temp4 == H2 && Count > Temp)
                { Temp1 = 0; Temp2 = 0; Temp3 = 0; Temp4 = 0; H1 = 0; H2 = 0; }
                return Function(gl, Counter, iteration);
            }
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