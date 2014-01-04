#include "Tuner.h"

Tuner::Tuner() {
	m_Tuning = new std::vector<int>(6);
	srand((unsigned)time(NULL));
}

Tuner::~Tuner(){
	delete m_Tuning;
}

void Tuner::setTuning(const std::vector<int>& tuning){
	m_Tuning = new std::vector<int>(tuning);
}

float Tuner::getError(int string){
	//int target_midi_note = (*m_Tuning)[string];
	// Arbitrary lower and higher tone limits
	float high = 0.5;
	float low = -0.5;
	// Random error between limits
	float error = (high - low) * ( (float)rand() / (float)RAND_MAX ) + low;
	return error;
}

EXTERNC TUNER_API Tuner* Tuner_New(){return new Tuner();}
EXTERNC TUNER_API void Tuner_Delete(Tuner* tuner){delete tuner;}