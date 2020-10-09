using System;
using System.Collections.Generic;
using System.Drawing;
using Tao.Glfw;
using Tao.OpenGl;

namespace _3D_Graph
{
    public class Lab
    {
        public int Width { get; set; }
        public int Height { get; set; }

        float alpha = 210.0f;
        float beta = -70.0f;
        float zoom = 2.0f;

        bool locked = false; //Для клика и переноса камеры по мышке
        bool isActive = true;

        int cursorX = 0;
        int cursorY = 0;
        double DEG2RAD = 3.14159265 / 180; //Градус к радиане

        public static LabBuilder CreateBuilder()
        {
            return new LabBuilder();
        }

        void FrameBufferSizeCallback(int aw, int ah)
        {
            // Параметры камеры:
            // угол обзора по вертикали (field of view angle)
            float fovY = 45.0f;
            // расстояние до ближайшей плоскости 
            float front = 0.1f;
            // расстояние до самой дальней плоскости 
            float back = 128.0f;
            // соотношение сторон экрана (width/height)
            float ratio = 1.0f;
            if (ah > 0)
                ratio = (float)aw / (float)ah;

            Gl.glViewport(0, 0, aw, ah);

            // Указывание, что матричные операции должны применяться к стеку проекционных матриц
            Gl.glMatrixMode(Gl.GL_PROJECTION);

            // Сбрасывание матрицы в ее состояние по умолчанию
            Gl.glLoadIdentity();

            double tang = Math.Tan(fovY / 2 * DEG2RAD);   // Тангенс половины угла обзора
            double heightF = front * tang;          // половина высоты до плоскости
            double widthF = heightF * ratio;      // половина длины до плоскости

            // Создание проекционнай матрицы на основе расстояния до ближайшей плоскости
            // сечения плоскости и распложения углов
            Gl.glFrustum(-widthF, widthF, -heightF, heightF, front, back);
        }

        void DrawHeatMap(List<Data> data)
        {
            // максимальное и минимальное значение в наборе данных
            float maxValue = -999.9f;
            float minValue = 999.9f;
            for (int i = 0; i < data.Count; i++)
            {
                Data d = data[i];
                if (d.Z > maxValue) //Если значение Z больше допустимого, увеличить допустимость
                {
                    maxValue = d.Z;
                }
                if (d.Z < minValue) //Так же, только в обратную сторону
                {
                    minValue = d.Z;
                }
            }
            //Вычисляем половину, на основе максимального и минимального значения, для масштабирования цвета
            float halfmax = (maxValue + minValue) / 2;

            // Отобразить результат
            Gl.glPointSize(3.0f);
            Gl.glBegin(Gl.GL_POINTS);
            float transparency = 0.25f;

            for (int i = 0; i < data.Count; i++)
            {
                //Получаем данные
                Data d = data[i];
                float value = d.Z;

                //На основе них задаем нужный нам цвет
                float b = 1.0f - value / halfmax;
                float r = value / halfmax - 1.0f;

                if (b < 0)
                {
                    b = 0;
                }
                if (r < 0)
                {
                    r = 0;
                }
                float g = 1.0f - b - r;

                //RGB
                Gl.glColor4f(r, g, b, transparency);
                //Рисуем примитив на основе координат data из dataset (List<Data>)
                Gl.glVertex3f(d.X, d.Y, d.Z);
            }
            Gl.glEnd();
        }

        void DrawOrigin()
        {
            float transparency = 0.5f;
            Gl.glLineWidth(4.0f);
            Gl.glBegin(Gl.GL_LINES);
            //Рисуем красную линию для Х координаты
            Gl.glColor4f(1.0f, 0.0f, 0.0f, transparency);
            Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            Gl.glColor4f(1.0f, 0.0f, 0.0f, transparency);
            Gl.glVertex3f(0.3f, 0.0f, 0.0f);

            //Рисуем зеленую линию для У координаты
            Gl.glColor4f(0.0f, 1.0f, 0.0f, transparency);
            Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            Gl.glColor4f(0.0f, 1.0f, 0.0f, transparency);
            Gl.glVertex3f(0.0f, 0.0f, 0.3f);

            //Рисуем синию линию для Z координаты
            Gl.glColor4f(0.0f, 0.0f, 1.0f, transparency);
            Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            Gl.glColor4f(0.0f, 0.0f, 1.0f, transparency);
            Gl.glVertex3f(0.0f, 0.3f, 0.0f);
            Gl.glEnd();
        }

        void KeyCallback(int key, int action)
        {
            //В зависимости от нажатой клавиши
            switch (key)
            {
                case Glfw.GLFW_KEY_LEFT://Стрелка влево
                    alpha += 5.0f;
                    break;
                case Glfw.GLFW_KEY_RIGHT://Стрелка вправо
                    alpha -= 5.0f;
                    break;
                case Glfw.GLFW_KEY_UP://Стрелка вверх
                    beta -= 5.0f;
                    break;
                case Glfw.GLFW_KEY_DOWN://Стрелка вниз
                    beta += 5.0f;
                    break;
                case Glfw.GLFW_KEY_PAGEUP://PageUp
                    zoom -= 0.25f;
                    if (zoom < 0.0f)
                        zoom = 0.0f;
                    break;
                case Glfw.GLFW_KEY_PAGEDOWN://PageDown
                    zoom += 0.25f;
                    break;
            }
        }

        void CursorPositionCallback(int x, int y)
        {
            //Если зажата ЛКМ
            if (locked)
            {
                //Меняем положение камеры
                alpha += (x - cursorX) / 10.0f;
                beta += (y - cursorY) / 10.0f;
            }

            cursorX = x;
            cursorY = y;
        }

        void ScrollCallback(int position)
        {
            zoom = -position;
            if (zoom < 0.0f)
                zoom = 0.0f;
        }

        int CloseCallback()
        {
            return 1;
        }

        void MouseCallback(int button, int action)
        {
            if (action == Glfw.GLFW_PRESS)
            {
                locked = true;
            }
            else
            {
                locked = false;
            }
        }

        public void Run(Bitmap bitmap)
        {
            Glfw.glfwInit();

            //Задаем параметры окна 
            Glfw.glfwOpenWindow(Width, Height, 8, 8, 8, 8, 0, 0, Glfw.GLFW_WINDOW);
            Gl.glViewport(0, 0, Width, Height);
            Glfw.glfwSetWindowTitle("3d function");

            Glfw.glfwSetKeyCallback(KeyCallback); //Колбек для клавы
            Glfw.glfwSetMousePosCallback(CursorPositionCallback); //Колбек для мышки
            Glfw.glfwSetMouseWheelCallback(ScrollCallback); //Колбек для скроллинга
            Glfw.glfwSetWindowSizeCallback(FrameBufferSizeCallback); //Колбек для резайзинга
            Glfw.glfwSetMouseButtonCallback(MouseCallback); //Колбек для нажатий клавиш
            
            Glfw.glfwSwapInterval(1);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);

            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glEnable(Gl.GL_ALPHA_TEST);

            Glfw.glfwSetWindowCloseCallback(CloseCallback);

            isActive = true;

            //Создать массив координат на основе картинки
            List<Data> data = createData(bitmap);

            while (isActive)
            {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();
                //двигаем объект взависости от зума
                Gl.glTranslatef(0.0f, 0.0f, -zoom);
                // вращаемся вокруг оси X 
                Gl.glRotatef(beta, 1.0f, 0.0f, 0.0f);
                // вращаемся вокруг оси Z
                Gl.glRotatef(alpha, 0.0f, 0.0f, 1.0f);

                //рисуем начало координат XYZ для визуализации
                DrawOrigin();

                //рисуем график
                DrawHeatMap(data);

                // Заменяем буфера для обновления экрана и обработки всех колбеков
                Glfw.glfwSwapBuffers();
                Glfw.glfwPollEvents();

                if (Glfw.glfwGetWindowParam(Glfw.GLFW_OPENED) == 0) isActive = false;
            }

            Glfw.glfwCloseWindow();
        }

        public void ShutDown()
        {
            isActive = false;
        }

        private List<Data> createData(Bitmap bm)
        {
            //Размер изображения
            int w = bm.Width;
            int h = bm.Height;

            //Размер сетки графика
            int ds_x = 500;
            int ds_y = 500;

            //Создание массива данных
            List<Data> data = new List<Data>(ds_x * ds_y);

            //Значение макисмальной высоты графика
            int maxHeigth = 10;

            int i = 0;
            for (float x = 0; x < ds_x; x++)
            {
                for (float y = 0; y < ds_y; y++)
                {
                    //Получить значение RGB текущего пикселя
                    Color c = bm.GetPixel((int)(x * w / ds_x), (int)(y * h / ds_y));

                    data.Add(new Data());

                    //Сохранение масштабированных координат
                    data[i].X = (x - (ds_x / 2)) * 50 / ds_x;
                    data[i].Y = (y - (ds_y / 2)) * 50 / ds_y;

                    //Высота вычисляется как среднеарифметическое каналов RGB
                    data[i].Z = ((float)(255 - (c.R + c.G + c.B) / 3)) / 255.0f * maxHeigth;
                    i++;
                }
            }
            return data;
        }
    }

    class Data
    {
        //Координаты
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}