#pragma once
#include "Tuner.h"
#include "Stdafx.h"

using namespace System;

namespace GPTunerWrapper {

	public ref class TunerWrapper
	{
	private:
		Tuner* m_Tuner;
	public:
		TunerWrapper() { m_Tuner = Tuner_New();}

		~TunerWrapper(){ Tuner_Delete(m_Tuner); }

		void setTuning(const std::vector<int>% tuning);
		float getError(int string);
	};
}
