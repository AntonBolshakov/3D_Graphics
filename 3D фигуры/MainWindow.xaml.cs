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


namespace Lab_work_3_4
{
    public partial class MainWindow : Window
    {
        private int G, F, U1, U2, U3, Y;
        private double W;
        private double H;
        private float rotation = 0.0f;

        public MainWindow()
        {
            InitializeComponent();
            F = 1;
            G = 1;
            Y = 1;
            U1 = 1;
            U2 = 1;
            U3 = 1;
        }

        private void openGLControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            Glut.glutInit();    // Инициализация Glut
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);   // установка режима отображения
            
            gl.ClearColor(0.8f, 0.8f, 0.8f, 0.0f);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            float[] pos = new float[4] { 3.0f, 1.0f, -19.0f, 0.9f };
            float[] pos1 = new float[3] { -1.0f, -1.0f, -1.0f };
            float[] material = new float[4] { 1.6f, 1.4f, 0.7f, 1.0f };

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, pos1);

            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, material);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, 128);
        }

        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);     // Задание матричного режима, GL_MODELVIEW - объектно-видовая матрица
            gl.PushMatrix();

            if (Y == 1) gl.ClearColor(0.8f, 0.8f, 0.8f, 0.0f);
            else if (Y == 2) gl.ClearColor(0.5f, 0.7f, 0.9f, 0.0f);
            else if (Y == 3) gl.ClearColor(0.9f, 0.5f, 0.7f, 0.0f);
            else gl.ClearColor(0.9f, 0.5f, 0f, 0.0f);

            if (G == 1) B8.Content = "Покраска";
            else if (G == 2) B8.Content = "Покраска и каркас";
            else B8.Content = "Каркас";

            if (U1 == 1) B12.Content = "Покраска 1 объекта";
            else if (U1 == 2) B12.Content = "Покраска и каркас 1 объекта";
            else B12.Content = "Каркас 1 объекта";

            if (U2 == 1) B13.Content = "Покраска 2 объекта";
            else if (U2 == 2) B13.Content = "Покраска и каркас 2 объекта";
            else B13.Content = "Каркас 2 объекта";

            if (U3 == 1) B14.Content = "Покраска 3 объекта";
            else if (U3 == 2) B14.Content = "Покраска и каркас 3 объекта";
            else B14.Content = "Каркас 3 объекта";

            if(F == 1)
            {
                gl.Color(0.9, 0, 0);        // Устанавливаем цвет объекта

                if (G == 2) Glut.glutSolidTeapot(2);    // Отрисовка чайника с помощью библиотеки FreeGLUT
                else if (G == 3)
                {
                    Glut.glutSolidTeapot(2);    // Отрисовка чайника с помощью библиотеки FreeGLUT
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireTeapot(2);

                gl.LoadIdentity();
                gl.Rotate(10, 2.0f, 0.0f, -2.0f);
                gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            }
            else if (F == 2)
            {
                gl.LoadIdentity();
                gl.Rotate(20, 1.0f, 0.0f, 0.0f);
                gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
                
                gl.Color(0.9, 0, 0);

                if (G == 2) Glut.glutSolidCube(3);
                else if (G == 3)
                {
                    Glut.glutSolidCube(3);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireCube(3); 
            }
            else if (F == 3)
            {
                gl.LoadIdentity();
                gl.Rotate(80, -30.0f, 25.0f, 20.0f);
                gl.Rotate(rotation, 0.0f, 0.0f, 1.0f);

                gl.Translate(0, 0, -2);

                gl.Color(0.9, 0, 0);        // Устанавливаем цвет объекта

                gl.Scale(0.4, 0.4, 0.4);
                if (G == 2) Glut.glutSolidCone(2.0, 12.0, 10, 0);
                else if (G == 3)
                {
                    Glut.glutSolidCone(2.0, 12.0, 10, 0);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireCone(2.0, 12.0, 10, 0);
            }
            else if (F == 4)
            {
                gl.LoadIdentity();
                gl.Rotate(0, 1.0f, 1.0f, 0.0f);
                gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

                gl.Color(0.9, 0, 0);        // Устанавливаем цвет объекта

                gl.Scale(2, 2, 2);
                if (G == 2) Glut.glutSolidIcosahedron();
                else if (G == 3)
                {
                    Glut.glutSolidIcosahedron();
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireIcosahedron();
            }
            else if (F == 5)
            {
                gl.LoadIdentity();
                gl.Rotate(0, 1.0f, 1.0f, 0.0f);
                gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

                gl.Color(0.9, 0, 0);        // Устанавливаем цвет объекта

                if (G == 2) Glut.glutSolidDodecahedron();
                else if (G == 3)
                {
                    Glut.glutSolidDodecahedron();
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireDodecahedron();

                gl.PopMatrix();     // Возврат сохраненной в стеке матрицы
                gl.Flush();     // Ожидание, пока библиотека OpenGL завершит визуализацию кадра, очистка буфера
            }
            else if(F == 6)
            {
                gl.LoadIdentity();
                gl.Rotate(0, 1.0f, 1.0f, 0.0f);
                gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

                gl.Color(0.9, 0, 0);        // Устанавливаем цвет объекта

                if (G == 2) Glut.glutSolidSphere(2, 15, 15);
                else if (G == 3)
                {
                    Glut.glutSolidSphere(2, 15, 15);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireSphere(2, 15, 15);

                gl.PopMatrix();     // Возврат сохраненной в стеке матрицы
                gl.Flush();     // Ожидание, пока библиотека OpenGL завершит визуализацию кадра, очистка буфера
            }
            else if (F == 7)
            {
                gl.LoadIdentity();
                gl.Rotate(30, 1, 1, 0);
                gl.Rotate(rotation, 0.0f, 0.0f, 1.0f);

                gl.Scale(1.5, 1.5, 1.5);
                gl.Color(0, 0.9, 0);
                if (U1 == 2) Glut.glutSolidDodecahedron();
                else if (U1 == 3)
                {
                    Glut.glutSolidDodecahedron();
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireDodecahedron();

                gl.Scale(0.4, 0.4, 0.4);
                gl.Color(0, 0, 0.9);
                if (U2 == 2) Glut.glutSolidCube(4);
                else if (U2 == 3)
                {
                    Glut.glutSolidCube(4);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireCube(4);

                gl.Scale(0.7, 0.7, 0.7);
                gl.Color(0.9, 0, 0);
                if (U3 == 2) Glut.glutSolidDodecahedron();
                else if (U3 == 3)
                {
                    Glut.glutSolidDodecahedron();
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireDodecahedron();

                gl.PopMatrix();
                gl.Flush();
            }
            else if(F == 8)
            {
                gl.LoadIdentity();
                gl.Rotate(90, 5, 1, 0);
                gl.Rotate(rotation, 0.0f, 0.0f, 1.0f);

                gl.Color(0.9, 0, 0);

                gl.Scale(0.07, 0.07, 0.07);
                if (U1 == 2) Glut.glutSolidTorus(5, 10, 10, 25);
                else if (U1 == 3)
                {
                    Glut.glutSolidTorus(5, 10, 10, 25);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireTorus(5, 10, 10, 15);

                gl.Translate(-45, -5, -5);

                gl.Color(0.9, 0, 0);

                gl.Color(0.9, 0.9, 0);
                if (U2 == 2) Glut.glutSolidSphere(20, 25, 25);
                else if (U2 == 3)
                {
                    Glut.glutSolidSphere(20, 25, 25);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireSphere(20, 25, 25);

                gl.PopMatrix();
                gl.Flush();
            }
            else
            {
                gl.LoadIdentity();
                gl.Rotate(20, 2.0f, 0.0f, -2.0f);
                gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

                gl.Color(0.9, 0, 0);

                if (U1 == 2) Glut.glutSolidTeapot(1.5);
                else if (U1 == 3)
                {
                    Glut.glutSolidTeapot(1.5);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireTeapot(1.5);

                gl.Color(0.9, 0.9, 0);
                if (U2 == 2) Glut.glutSolidSphere(3, 15, 15);
                else if (U2 == 3)
                {
                    Glut.glutSolidSphere(3, 15, 15);
                    gl.Color(0, 0, 0);
                }
                Glut.glutWireSphere(3, 15, 15);

                gl.PopMatrix();
                gl.Flush();
            }

            rotation += 3.0f;
        }

        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
        }

        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        { }

        private void openGLControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(37.0f, (double)Width / (double)Height, 0.01, 100.0);
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        { Y = 1; F = 1; }

        private void B2_Click(object sender, RoutedEventArgs e)
        { Y = 1; F = 2; }

        private void B3_Click(object sender, RoutedEventArgs e)
        { Y = 1; F = 3; }

        private void B4_Click(object sender, RoutedEventArgs e)
        { Y = 1; F = 4; }

        private void B5_Click(object sender, RoutedEventArgs e)
        { Y = 1; F = 5; }

        private void B6_Click(object sender, RoutedEventArgs e)
        { Y = 1; F = 6; }

        private void B8_Click(object sender, RoutedEventArgs e)
        {
            if (G == 1) G = 2;
            else if (G == 2) G = 3;
            else G = 1;
        }

        private void B9_Click(object sender, RoutedEventArgs e)
        { Y = 0; F = 7; }

        private void B10_Click(object sender, RoutedEventArgs e)
        { Y = 2; F = 8; }

        private void B11_Click(object sender, RoutedEventArgs e)
        { Y = 3; F = 9; }

        private void B12_Click(object sender, RoutedEventArgs e)
        {
            if (U1 == 1) U1 = 2;
            else if (U1 == 2) U1 = 3;
            else U1 = 1;
        }

        private void B13_Click(object sender, RoutedEventArgs e)
        {
            if (U2 == 1) U2 = 2;
            else if (U2 == 2) U2 = 3;
            else U2 = 1;
        }

        private void B14_Click(object sender, RoutedEventArgs e)
        {
            if (U3 == 1) U3 = 2;
            else if (U3 == 2) U3 = 3;
            else U3 = 1;
        } 
    }
}
