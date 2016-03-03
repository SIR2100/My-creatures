#include <windows.h>
#include <GL/glut.h>

GLfloat angleCube = 0.0f;
int refreshMills = 15;

//�������������� OpenGL � �����������
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
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); // ������� Color � Depth ������
	glMatrixMode(GL_MODELVIEW);     // ��������� ���

	//���������� ������� ���� (�����������)

	glLoadIdentity();                 // ���������� ������� � �������� ���
	glTranslatef(2.0f, 0.0f, -7.0f);  // 1% - x, 2% - y, 3% - ������
	glRotatef(angleCube, 1.0f, 1.0f, 1.0f);  // �������

	glBegin(GL_QUADS);                // ��������, �������������� ���

	// ����
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, 1.0f, -1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, 1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, 1.0f, 1.0f);

	// ���
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, -1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, -1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, -1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, -1.0f, -1.0f);

	// ��������
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, 1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, -1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, -1.0f, 1.0f);

	// ������
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, -1.0f, -1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, 1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, 1.0f, -1.0f);

	// �����
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(-1.0f, 1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 0.5f); glVertex3f(-1.0f, -1.0f, -1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(-1.0f, -1.0f, 1.0f);

	// ������
	glColor3f(1.0f, 0.5f, 0.0f); glVertex3f(1.0f, 1.0f, -1.0f);
	glColor3f(1.0f, 0.0f, 1.0f); glVertex3f(1.0f, 1.0f, 1.0f);
	glColor3f(0.5f, 0.5f, 0.0f); glVertex3f(1.0f, -1.0f, 1.0f);
	glColor3f(0.0f, 0.5f, 1.0f); glVertex3f(1.0f, -1.0f, -1.0f);
	glEnd();

	//���������� ������ ���� (�������� �����)
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


	glLoadIdentity();                 // ���������� ������� � �������� ���
	glTranslatef(-2.0f, 0.0f, -7.0f);  // 1% - x, 2% - y, 3% - ������
	glRotatef(angleCube, -1.0f, 1.0f, 1.0f);  // �������

	glBegin(GL_QUADS);                // ��������, �������������� ���

	// ����
	glColor3f(colorPlus*0.5, colorMinus*0.5, colorMinus*0.5);
	glVertex3f(1.0f, 1.0f, -1.0f);
	glVertex3f(-1.0f, 1.0f, -1.0f);
	glVertex3f(-1.0f, 1.0f, 1.0f);
	glVertex3f(1.0f, 1.0f, 1.0f);

	// ���
	glColor3f(colorMinus*0.5, colorPlus*0.5, colorMinus*0.5);
	glVertex3f(1.0f, -1.0f, 1.0f);
	glVertex3f(-1.0f, -1.0f, 1.0f);
	glVertex3f(-1.0f, -1.0f, -1.0f);
	glVertex3f(1.0f, -1.0f, -1.0f);

	// ��������
	glColor3f(colorMinus*0.5, colorMinus*0.5, colorPlus*0.5);
	glVertex3f(1.0f, 1.0f, 1.0f);
	glVertex3f(-1.0f, 1.0f, 1.0f);
	glVertex3f(-1.0f, -1.0f, 1.0f);
	glVertex3f(1.0f, -1.0f, 1.0f);

	// ������
	glColor3f(colorPlus*0.5, colorPlus*0.5, colorMinus*0.5);
	glVertex3f(1.0f, -1.0f, -1.0f);
	glVertex3f(-1.0f, -1.0f, -1.0f);
	glVertex3f(-1.0f, 1.0f, -1.0f);
	glVertex3f(1.0f, 1.0f, -1.0f);

	// �����
	glColor3f(colorMinus*0.5, colorPlus*0.5, colorPlus*0.5);
	glVertex3f(-1.0f, 1.0f, 1.0f);
	glVertex3f(-1.0f, 1.0f, -1.0f);
	glVertex3f(-1.0f, -1.0f, -1.0f);
	glVertex3f(-1.0f, -1.0f, 1.0f);

	// ������
	glColor3f(colorPlus*0.5, colorMinus*0.5, colorPlus*0.5);
	glVertex3f(1.0f, 1.0f, -1.0f);
	glVertex3f(1.0f, 1.0f, 1.0f);
	glVertex3f(1.0f, -1.0f, 1.0f);
	glVertex3f(1.0f, -1.0f, -1.0f);
	glEnd();

	glutSwapBuffers(); //������� �����������, ������ ������� ������� � ��������

	angleCube -= 0.5f; //���� ��������
}

//������
void timer(int value) {
	glutPostRedisplay();      // �������������� ����
	glutTimerFunc(refreshMills, timer, 0); // ����� �������� ������ �� ������
}

//Reshape �������
void reshape(GLsizei width, GLsizei height) {  //����� GLsizei ��� ���������� �����
	// ������� ����������� ������
	if (height == 0) height = 1; //������ �� "������"
	GLfloat aspect = (GLfloat)width / (GLfloat)height;

	// ������ ����� ��� Viewport
	glViewport(0, 0, width, height);

	// �� �������� � ������������
	glMatrixMode(GL_PROJECTION);  // ������� ��������
	glLoadIdentity();             // Reset
	// �����������: fovy, aspect, zNear and zFar
	gluPerspective(45.0f, aspect, 0.1f, 100.0f);
}

//Main
int main(int argc, char** argv) {
	glutInit(&argc, argv);            // ��������� Glut
	glutInitDisplayMode(GLUT_DOUBLE); // �������� ������� ������������
	glutInitWindowSize(640, 480);   // �������� ���������� ����
	glutInitWindowPosition(50, 50); // ������� ������������ ������-�������� ���� ������ � ��������
	glutCreateWindow("4. ���, �������� �������. 5. ��� � ����������� ��������");
	glutDisplayFunc(display);       // Call-Back ��� �������������
	glutReshapeFunc(reshape);       // Re-Size
	initGL();                       // ���������� OpenGL
	glutTimerFunc(0, timer, 0);     // ����� ��������� ������
	glutMainLoop();                 // ��������� ����������� ����
	return 0;
}