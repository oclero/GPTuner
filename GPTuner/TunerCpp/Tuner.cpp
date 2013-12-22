#include "Tuner.h"

GPTuner::Tuner::Tuner() {
	m_Tuning = new std::vector<int>(6);
}

GPTuner::Tuner::~Tuner(){
	delete m_Tuning;
}

void GPTuner::Tuner::setTuning(const std::vector<int>& tuning){
	m_Tuning = new std::vector<int>(tuning);
}

float GPTuner::Tuner::getError(int string){
	int target_midi_note = (*m_Tuning)[string];
	// Arbitrary lower and higher tone limits
	float high = 2.0;
	float low = -2.0;
	// Random error between limits
	float random_error = low + static_cast <float> (rand()) /( static_cast <float> (RAND_MAX/(high-low)));
	return random_error;
}

EXTERNC TUNER_API GPTuner::Tuner* GPTuner::Tuner_New(){return new Tuner();}
EXTERNC TUNER_API void GPTuner::Tuner_Delete(Tuner* tuner){delete tuner;}