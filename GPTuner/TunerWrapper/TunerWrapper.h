#pragma once
#include "Tuner.h"
#include "Stdafx.h"

using namespace System;

namespace GPTunerWrapper {

	public ref class TunerWrapper
	{
	private:
		GPTuner::Tuner* m_Tuner;
	public:
		TunerWrapper() { m_Tuner = GPTuner::Tuner_New();}

		~TunerWrapper(){ GPTuner::Tuner_Delete(m_Tuner); }

		void setTuning(const std::vector<int>% tuning);
		float getError(int string);
	};
}
