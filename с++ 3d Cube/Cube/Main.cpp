#include <windows.h>
#include <GL/glut.h>

GLfloat angleCube = 0.0f;
int refreshMills = 15;

//Инициализируем OpenGL и настраиваем
void initGL() {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f); 
	glClearDepth(1.0f);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LEQUAL); 
	glShadeModel(GL_SMOOTH);
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);
}

double colorPlus = 0;
double colorMinus = 1;
bool up = true;

void display() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); // Очищаем Color и Depth буферы
	glMatrixMode(GL_MODELVIEW);     // Матричный вид

	//Построение правого куба (Градиентный)

	glLoadIdentity();                 // Возвращаем матрицу в исходный вид
	glTranslatef(2.0f, 0.0f, -7.0f);  // 1% - x, 2% - y, 3% - размер
	glRotatef(angleCube, 1.0f, 1.0f, 1.0f);  // Вращаем

	glBegin(GL_QUADS);                // Фунцкция, отрисовывающая куб

	// Верх
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, 1.0f, -1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, 1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, 1.0f, 1.0f);

	// Низ
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, -1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, -1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, -1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, -1.0f, -1.0f);

	// Передний
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, 1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, -1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, -1.0f, 1.0f);

	// Задний
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, -1.0f, -1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, 1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, 1.0f, -1.0f);

	// Левый
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, -1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(-1.0f, -1.0f, 1.0f);

	// Правый
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, 1.0f, -1.0f);
	glColor3f(1.0f, 0.0f, 1.0f); glVertex3f(1.0f, 1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(1.0f, -1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, -1.0f, -1.0f);
	glEnd();

	//Построение левого куба (Меняющий цвета)
	if (up == true)
	{
		colorPlus += 0.01;
		colorMinus -= 0.01;
		if (colorPlus > 0.99) { up = false; }
	}
	else
	{
		colorPlus -= 0.01;
		colorMinus += 0.01;
		if (colorPlus < 0.01) { up = true; }
	}


	glLoadIdentity();                 // Возвращаем матрицу в исходный вид
	glTranslatef(-2.0f, 0.0f, -7.0f);  // 1% - x, 2% - y, 3% - размер
	glRotatef(angleCube, -1.0f, 1.0f, 1.0f);  // Вращаем

	glBegin(GL_QUADS);                // Фунцкция, отрисовывающая куб

	// Верх
	glColor3f(colorPlus*0.5, colorMinus*0.5, colorMinus*0.5);
	glVertex3f(1.0f, 1.0f, -1.0f);
	glVertex3f(-1.0f, 1.0f, -1.0f);
	glVertex3f(-1.0f, 1.0f, 1.0f);
	glVertex3f(1.0f, 1.0f, 1.0f);

	// Низ
	glColor3f(colorMinus*0.5, colorPlus*0.5, colorMinus*0.5);
	glVertex3f(1.0f, -1.0f, 1.0f);
	glVertex3f(-1.0f, -1.0f, 1.0f);
	glVertex3f(-1.0f, -1.0f, -1.0f);
	glVertex3f(1.0f, -1.0f, -1.0f);

	// Передний
	glColor3f(colorMinus*0.5, colorMinus*0.5, colorPlus*0.5);
	glVertex3f(1.0f, 1.0f, 1.0f);
	glVertex3f(-1.0f, 1.0f, 1.0f);
	glVertex3f(-1.0f, -1.0f, 1.0f);
	glVertex3f(1.0f, -1.0f, 1.0f);

	// Задний
	glColor3f(colorPlus*0.5, colorPlus*0.5, colorMinus*0.5);
	glVertex3f(1.0f, -1.0f, -1.0f);
	glVertex3f(-1.0f, -1.0f, -1.0f);
	glVertex3f(-1.0f, 1.0f, -1.0f);
	glVertex3f(1.0f, 1.0f, -1.0f);

	// Левый
	glColor3f(colorMinus*0.5, colorPlus*0.5, colorPlus*0.5);
	glVertex3f(-1.0f, 1.0f, 1.0f);
	glVertex3f(-1.0f, 1.0f, -1.0f);
	glVertex3f(-1.0f, -1.0f, -1.0f);
	glVertex3f(-1.0f, -1.0f, 1.0f);

	// Правый
	glColor3f(colorPlus*0.5, colorMinus*0.5, colorPlus*0.5);
	glVertex3f(1.0f, 1.0f, -1.0f);
	glVertex3f(1.0f, 1.0f, 1.0f);
	glVertex3f(1.0f, -1.0f, 1.0f);
	glVertex3f(1.0f, -1.0f, -1.0f);
	glEnd();

	glutSwapBuffers(); //Двойная буферизация, меняем лицевую сторону с обратной

	angleCube -= 0.5f; //Угол поворота
}

//Таймер
void timer(int value) {
	glutPostRedisplay();      // Перерисовываем окно
	glutTimerFunc(refreshMills, timer, 0); // Снова вызываем таймер мс спустя
}

//Reshape функция
void reshape(GLsizei width, GLsizei height) {  //Юзаем GLsizei для корректных чисел
	// Считаем соотношение сторон
	if (height == 0) height = 1; //Защита от "дурака"
	GLfloat aspect = (GLfloat)width / (GLfloat)height;

	// Задаем вывод для Viewport
	glViewport(0, 0, width, height);

	// Не забываем о соотношениях
	glMatrixMode(GL_PROJECTION);  // Матрица проекций
	glLoadIdentity();             // Reset
	// Перспективы: fovy, aspect, zNear and zFar
	gluPerspective(45.0f, aspect, 0.1f, 100.0f);
}

//Main
int main(int argc, char** argv) {
	glutInit(&argc, argv);            // Подключем Glut
	glutInitDisplayMode(GLUT_DOUBLE); // Включаем двойную буфферизацию
	glutInitWindowSize(640, 480);   // Исходное разрешение окна
	glutInitWindowPosition(50, 50); // Позиция относительно левого-верхнего угла экрана в пикселях
	glutCreateWindow("4. Куб, меняющий окраску. 5. Куб с градиентной заливкой");
	glutDisplayFunc(display);       // Call-Back для переотрисовки
	glutReshapeFunc(reshape);       // Re-Size
	initGL();                       // Подключаем OpenGL
	glutTimerFunc(0, timer, 0);     // Сразу запускаем таймер
	glutMainLoop();                 // Запускаем бесконечный цикл
	return 0;
}