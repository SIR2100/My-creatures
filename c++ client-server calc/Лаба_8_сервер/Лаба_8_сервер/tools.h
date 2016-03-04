#ifndef TOOLS_H
#define TOOLS_H

#include <vector>
#include <string>
#include <queue>
#include <functional>

#pragma comment(lib, "Ws2_32.lib")

namespace tools_const {
	const int maxlen = 30;
	const std::string end_of_query = "__END_OF_QUERY__";
}

std::string parse_query(std::vector<char *> data);
std::vector<char *> build_query(std::string & s);
char * replace_char(char *s, char find, char replace);
std::string clear_string(std::string & s);

#endif // !TOOLS_H