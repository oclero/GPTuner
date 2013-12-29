#pragma once
#define TUNER_DLL_EXPORT

#ifdef TUNER_DLL_EXPORT
    #define TUNER_API __declspec(dllexport)
	#define EXTERNC extern "C"
#else
    #define TUNER_API __declspec(dllimport)
	#define EXTERNC
#endif

#include <stdio.h>      /* printf, NULL */
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */
#include <vector>

namespace GPTuner{
	class TUNER_API Tuner {
	private:
		std::vector<int>* m_Tuning;
	public :
		Tuner();
		~Tuner();
		void setTuning(const std::vector<int>& tuning);
		float getError(int string);
	};
	EXTERNC TUNER_API Tuner* Tuner_New();
	EXTERNC TUNER_API void Tuner_Delete(Tuner* tuner);
}