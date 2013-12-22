#pragma once
#include "Tuner.h"

using namespace System;

namespace GPTunerWrapper {

	public ref class TunerWrapper
	{
	private:
		GPTuner::Tuner* m_Tuner;
	public:
		TunerWrapper();
		~TunerWrapper();
		void setTuning(const std::vector<int>& tuning);
		float getError(int string);
	};
}
