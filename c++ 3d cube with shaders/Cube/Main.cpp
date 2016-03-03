#include "GL/glew.h"
#include "GL/glut.h"
#include "glm/gtc/matrix_transform.hpp"
#include <iostream>

#include "GlShader.h"

GlShader shader;
GLint  Attrib_vertex;
GLint  Attrib_color;
GLint  Unif_matrix;
GLuint VBO_vertex;
GLuint VBO_color;
GLuint VBO_element;
GLint Indices_count;
mat4 Matrix_projection;
GLfloat angleCube = 0.0f;
int x = 0;
int refreshMills = 15;

double colorR = 0.01;
double colorG = 0.01;
double colorB = 0.01;
bool flagR = true;
bool flagG = true;
bool flagB = true;


struct vertex { GLfloat x; GLfloat y; GLfloat z; };

void initGL()
{
	glClearColor(0, 0, 0.1, 0);
	glEnable(GL_DEPTH_TEST);
}

//====================================================== Инициализация шейдеров ===============================================
void initShader()
{
	// Исходник шейдеров
	const GLchar* vsSource =
		"attribute vec3 coord;\n"
		"attribute vec3 color;\n"
		"varying vec3 var_color;\n"
		"uniform mat4 matrix;\n"
		"uniform vec3  camera_position;\n"
		"void main() {\n"
		"  gl_Position = matrix * vec4(coord*1.5, 1.0);\n"
		"  var_color = color;\n"
		"}\n";
		//"attribute  vec3  in_vertex_position;\n"
		//"attribute  vec2  in_vertex_textureCoord;\n"
		//"attribute  vec3  in_vertex_normal;\n"
		//"uniform    mat4  camera_matrix;\n"
		//"uniform    vec3  camera_position;\n"
		//"uniform    vec3  light_position;\n"
		//"varying    vec2  vertex_textureCoord;\n"
		//"varying    vec3  vertex_normal;\n"
		//"varying    vec3  light_direction;\n"
		//"varying    vec3  view_direction;\n"
		//"void main(void) {\n"
		//"	gl_Position = camera_matrix*vec4(in_vertex_position, 1.0);\n"
		//"	vertex_textureCoord = in_vertex_textureCoord;\n"
		//"	vertex_normal = vec3(camera_matrix*vec4(in_vertex_normal, 1.0));\n"
		//"	light_direction = normalize(light_position - in_vertex_position);\n"
		//"	view_direction = normalize(camera_position - in_vertex_position);\n"
		//"}\n";

	const GLchar* fsSource =
		"varying vec3 var_color;\n"
		"void main() {\n"
		"  gl_FragColor = vec4(var_color*2, 1.0);\n"
		"}\n";
		/*"varying    vec2    vertex_textureCoord;\n"
		"varying    vec3    vertex_normal;\n"
		"varying    vec3    light_direction;\n"
		"varying    vec3    view_direction;\n"
		"uniform    sampler2D  Texture;\n"
		"uniform    vec4    light_ambient;\n"
		"uniform    vec4    light_diffuse;\n"
		"uniform    vec4    light_specular;\n"
		"uniform    vec3    light_attenuation;\n"
		"uniform    vec4    material_ambient;\n"
		"uniform    vec4    material_diffuse;\n"
		"uniform    vec4    material_specular;\n"
		"uniform    vec4    material_emission;\n"
		"uniform    float    material_shininess;\n"
		"void main(void) {\n"
		"	gl_FragColor = material_emission + material_ambient*light_ambient*light_attenuation;\n"
		"	float NdotL = max(dot(vertex_normal, light_direction), 0.0);\n"
		"	gl_FragColor += material_diffuse*light_diffuse*NdotL*light_attenuation;\n"
		"	float RdotVpow = max(pow(dot(reflect(-light_direction, vertex_normal), view_direction), material_shininess), 0.0);\n"
		"	gl_FragColor += material_specular*light_specular*RdotVpow*light_attenuation;\n"
		"	gl_FragColor *= texture(Texture, vertex_textureCoord);\n"
		"}\n";*/
	shader.load(vsSource, fsSource);

	Attrib_vertex = shader.getAttribLocation("coord");
	Attrib_color = shader.getAttribLocation("color");
	Unif_matrix = shader.getUniformLocation("matrix");
}

//====================================================== инициализация VBO ===============================================
void Display(){

	if ((colorR <= 1) && (colorR > 0.001))
	{
		if (flagR) { colorR = colorR + 0.01; }
		else { colorR = colorR - 0.01; }

		if (colorR >= 0.99) { flagR = false; }
	}
	else if ((colorG <= 1) && (colorG > 0.001))
	{
		if (flagG) { colorG = colorG + 0.01; }
		else { colorG = colorG - 0.01; }

		if (colorG >= 0.99) { flagG = false; }
	}
	else if (colorB <= 1)
	{
		if (flagB) { colorB = colorB + 0.01; }
		else { colorB = colorB - 0.01; }

		if (colorB >= 0.99) { flagB = false; }

		if ((colorB <= 0.01) && (flagB == false)) { flagR = true; flagG = true; flagB = true; colorR = 0.01; colorG = 0.01; colorB = 0.01; }
	}

	//! Вершины куба
	vertex vertices[] = {
		{ -1.0f, -1.0f, -1.0f }, { 1.0f, -1.0f, -1.0f }, { 1.0f, 1.0f, -1.0f }, { -1.0f, 1.0f, -1.0f },
		{ -1.0f, -1.0f, 1.0f }, { 1.0f, -1.0f, 1.0f }, { 1.0f, 1.0f, 1.0f }, { -1.0f, 1.0f, 1.0f }
	};
	//! Цвета куба без альфа компонента
	vertex colors[] = {
		{ colorR*0.1f, colorG*0.1f, colorB*0.1f }, { colorR*0.3f, colorG*0.3f, colorB*0.3f }, { colorR*0.5f, colorG*0.5f, colorB*0.5f }, { colorR*0.9f, colorG*0.9f, colorB*0.9f },
		{ colorR*0.2f, colorG*0.2f, colorB*0.2f }, { colorR*0.4f, colorG*0.4f, colorB*0.4f }, { colorR*0.7f, colorG*0.7f, colorB*0.7f }, { colorR*0.6f, colorG*0.6f, colorB*0.6f },
	};
	//! Индексы вершин, обшие и для цветов
	GLint indices[] = { 0, 4, 5, 0, 5, 1, 1, 5, 6, 1, 6, 2, 2, 6, 7, 2, 7, 3, 3, 7, 4, 3, 4, 0, 4, 7, 6, 4, 6, 5, 3, 0, 1, 3, 1, 2 };

	// Создаем буфер для вершин
	glGenBuffers(1, &VBO_vertex);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	// Создаем буфер для цветов вершин
	glGenBuffers(1, &VBO_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);

	// Создаем буфер для индексов вершин
	glGenBuffers(1, &VBO_element);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_element);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	Indices_count = sizeof(indices) / sizeof(indices[0]);
}

//====================================================== Вращаем кубик ===============================================

void timer(int value) {
	Display();
	Matrix_projection = glm::perspective(30.0f, 1.0f, 1.0f, 200.0f);
	// Перемещаем центр нашей оси координат для того чтобы увидеть куб
	Matrix_projection = glm::translate(Matrix_projection, vec3(0.0f, 0.0f, -10.0f));
	// Поворачиваем ось координат(тоесть весь мир), чтобы развернуть отрисованное
	Matrix_projection = glm::rotate(Matrix_projection, 1.0f*x, vec3(1.0f, 1.0f, 0.0f));
	x = x + 1;
	glutTimerFunc(refreshMills, timer, 0); // Снова вызываем таймер мс спустя
	glutPostRedisplay();
}

//====================================================== Отрисовка кубика ===============================================
void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	shader.use();
	shader.setUniform(Unif_matrix, Matrix_projection);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_element);
	glEnableVertexAttribArray(Attrib_vertex);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	glVertexAttribPointer(Attrib_vertex, 3, GL_FLOAT, GL_FALSE, 0, 0);
	glEnableVertexAttribArray(Attrib_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glVertexAttribPointer(Attrib_color, 3, GL_FLOAT, GL_FALSE, 0, 0);


	// Отрисовываем
	glDrawElements(GL_TRIANGLES, Indices_count, GL_UNSIGNED_INT, 0);

	glutSwapBuffers();
	angleCube -= 0.5f; // Угол поворота
}

//Reshape функция
void reshape(GLsizei width, GLsizei height) {  //Юзаем GLsizei для корректных чисел
	// Считаем соотношение сторон
	if (height >= width)
	{
		height = width;
	}
	else
	{
		width = height;
	}
	// Задаем вывод для Viewport
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(-2.0, 2.0, -2.0, 2.0, -2.0, 2.0);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
}

//====================================================== Просто Main ===============================================
int main(int argc, char **argv)
{


	glutInit(&argc, argv);					// Подключем Glut 
	glutInitDisplayMode(GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(600, 600);			// Размер окна 
	glutCreateWindow("2 и 3 задания");		// Название окна
	GLenum glew_status = glewInit();
	glutDisplayFunc(render);				// Call-Back для переотрисовки
	glutReshapeFunc(reshape);		        // Re-Size
	initGL();								// Подключаем OpenGL
	Display();								// Подключаем Vertex буффер
	initShader();							// Инициализируем шейдеры
	glutTimerFunc(15, timer, 0);			// Сразу запускаем таймер
	glutMainLoop();							// Запускаем бесконечный цикл
	return 0;
}
