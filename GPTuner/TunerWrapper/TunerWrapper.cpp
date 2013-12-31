#include "Stdafx.h"
#include "TunerWrapper.h"

void GPTunerWrapper::TunerWrapper::setTuning(const std::vector<int>% tuning){
	m_Tuner->setTuning(tuning);
}

float GPTunerWrapper::TunerWrapper::getError(int string){
	return m_Tuner->getError(string);
}