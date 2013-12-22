
#include "stdafx.h"

#include "TunerWrapper.h"
#include "Tuner.h"

GPTunerWrapper::TunerWrapper::TunerWrapper(){
	m_Tuner = GPTuner::Tuner_New();
}

GPTunerWrapper::TunerWrapper::~TunerWrapper(){
	GPTuner::Tuner_Delete(m_Tuner);
}

void GPTunerWrapper::TunerWrapper::setTuning(const std::vector<int>& tuning){
	m_Tuner->setTuning(tuning);
}

float GPTunerWrapper::TunerWrapper::getError(int string){
	return m_Tuner->getError(string);
}