package net.sourceforge.jaad.aac.ps;

interface PSTables extends PSConstants {

	/* type definitaions */
	/* static data tables */
	int[] nr_iid_par_tab = {
		10, 20, 34, 10, 20, 34, 0, 0
	};
	int[] nr_icc_par_tab = {
		10, 20, 34, 10, 20, 34, 0, 0
	};
	int[] nr_ipdopd_par_tab = {
		5, 11, 17, 5, 11, 17, 0, 0
	};
	int[][] num_env_tab = {
		{0, 1, 2, 4},
		{1, 2, 3, 4}
	};
	float[] filter_a = { /* a(m) = exp(-d_48kHz(m)/7) */
		0.65143905753106f,
		0.56471812200776f,
		0.48954165955695f
	};

	int[] group_border20 = {
		6, 7, 0, 1, 2, 3, /* 6 subqmf subbands */
		9, 8, /* 2 subqmf subbands */
		10, 11, /* 2 subqmf subbands */
		3, 4, 5, 6, 7, 8, 9, 11, 14, 18, 23, 35, 64
	};

	int[] group_border34 = {
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, /* 12 subqmf subbands */
		12, 13, 14, 15, 16, 17, 18, 19, /*  8 subqmf subbands */
		20, 21, 22, 23, /*  4 subqmf subbands */
		24, 25, 26, 27, /*  4 subqmf subbands */
		28, 29, 30, 31, /*  4 subqmf subbands */
		32-27, 33-27, 34-27, 35-27, 36-27, 37-27, 38-27, 40-27, 42-27, 44-27, 46-27, 48-27, 51-27, 54-27, 57-27, 60-27, 64-27, 68-27, 91-27
	};

	int[] map_group2bk20 = {
		(NEGATE_IPD_MASK|1), (NEGATE_IPD_MASK|0),
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
	};

	int[] map_group2bk34 = {
		0, 1, 2, 3, 4, 5, 6, 6, 7, (NEGATE_IPD_MASK|2), (NEGATE_IPD_MASK|1), (NEGATE_IPD_MASK|0),
		10, 10, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 9,
		14, 11, 12, 13,
		14, 15, 16, 13,
		16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33
	};

	int[] delay_length_d = {3, 4, 5 /* d_48kHz */};
	/* tables */
	/* filters are mirrored in coef 6, second half left out */
	float[] p8_13_20 = {
		0.00746082949812f,
		0.02270420949825f,
		0.04546865930473f,
		0.07266113929591f,
		0.09885108575264f,
		0.11793710567217f,
		0.125f
	};

	float[] p2_13_20 = {
		0.0f,
		0.01899487526049f,
		0.0f,
		-0.07293139167538f,
		0.0f,
		0.30596630545168f,
		0.5f
	};

	float[] p12_13_34 = {
		0.04081179924692f,
		0.03812810994926f,
		0.05144908135699f,
		0.06399831151592f,
		0.07428313801106f,
		0.08100347892914f,
		0.08333333333333f
	};

	float[] p8_13_34 = {
		0.01565675600122f,
		0.03752716391991f,
		0.05417891378782f,
		0.08417044116767f,
		0.10307344158036f,
		0.12222452249753f,
		0.125f
	};

	float[] p4_13_34 = {
		-0.05908211155639f,
		-0.04871498374946f,
		0.0f,
		0.07778723915851f,
		0.16486303567403f,
		0.23279856662996f,
		0.25f
	};

	/* RE(ps->Phi_Fract_Qmf[j]) = (float)cos(M_PI*(j+0.5)*(0.39)); */
	/* IM(ps->Phi_Fract_Qmf[j]) = (float)sin(M_PI*(j+0.5)*(0.39)); */
	float[][] Phi_Fract_Qmf = {
		{0.8181497455f, 0.5750052333f},
		{-0.2638730407f, 0.9645574093f},
		{-0.9969173074f, 0.0784590989f},
		{-0.4115143716f, -0.9114032984f},
		{0.7181262970f, -0.6959127784f},
		{0.8980275989f, 0.4399391711f},
		{-0.1097343117f, 0.9939609766f},
		{-0.9723699093f, 0.2334453613f},
		{-0.5490227938f, -0.8358073831f},
		{0.6004202366f, -0.7996846437f},
		{0.9557930231f, 0.2940403223f},
		{0.0471064523f, 0.9988898635f},
		{-0.9238795042f, 0.3826834261f},
		{-0.6730124950f, -0.7396311164f},
		{0.4679298103f, -0.8837656379f},
		{0.9900236726f, 0.1409012377f},
		{0.2027872950f, 0.9792228341f},
		{-0.8526401520f, 0.5224985480f},
		{-0.7804304361f, -0.6252426505f},
		{0.3239174187f, -0.9460853338f},
		{0.9998766184f, -0.0157073177f},
		{0.3534748554f, 0.9354440570f},
		{-0.7604059577f, 0.6494480371f},
		{-0.8686315417f, -0.4954586625f},
		{0.1719291061f, -0.9851093292f},
		{0.9851093292f, -0.1719291061f},
		{0.4954586625f, 0.8686315417f},
		{-0.6494480371f, 0.7604059577f},
		{-0.9354440570f, -0.3534748554f},
		{0.0157073177f, -0.9998766184f},
		{0.9460853338f, -0.3239174187f},
		{0.6252426505f, 0.7804304361f},
		{-0.5224985480f, 0.8526401520f},
		{-0.9792228341f, -0.2027872950f},
		{-0.1409012377f, -0.9900236726f},
		{0.8837656379f, -0.4679298103f},
		{0.7396311164f, 0.6730124950f},
		{-0.3826834261f, 0.9238795042f},
		{-0.9988898635f, -0.0471064523f},
		{-0.2940403223f, -0.9557930231f},
		{0.7996846437f, -0.6004202366f},
		{0.8358073831f, 0.5490227938f},
		{-0.2334453613f, 0.9723699093f},
		{-0.9939609766f, 0.1097343117f},
		{-0.4399391711f, -0.8980275989f},
		{0.6959127784f, -0.7181262970f},
		{0.9114032984f, 0.4115143716f},
		{-0.0784590989f, 0.9969173074f},
		{-0.9645574093f, 0.2638730407f},
		{-0.5750052333f, -0.8181497455f},
		{0.5750052333f, -0.8181497455f},
		{0.9645574093f, 0.2638730407f},
		{0.0784590989f, 0.9969173074f},
		{-0.9114032984f, 0.4115143716f},
		{-0.6959127784f, -0.7181262970f},
		{0.4399391711f, -0.8980275989f},
		{0.9939609766f, 0.1097343117f},
		{0.2334453613f, 0.9723699093f},
		{-0.8358073831f, 0.5490227938f},
		{-0.7996846437f, -0.6004202366f},
		{0.2940403223f, -0.9557930231f},
		{0.9988898635f, -0.0471064523f},
		{0.3826834261f, 0.9238795042f},
		{-0.7396311164f, 0.6730124950f}
	};

	/* RE(Phi_Fract_SubQmf20[j]) = (float)cos(M_PI*f_center_20[j]*0.39); */
	/* IM(Phi_Fract_SubQmf20[j]) = (float)sin(M_PI*f_center_20[j]*0.39); */
	float[][] Phi_Fract_SubQmf20 = {
		{0.9882950187f, 0.1525546312f},
		{0.8962930441f, 0.4434623122f},
		{0.7208535671f, 0.6930873394f},
		{0.4783087075f, 0.8781917691f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{0.8962930441f, -0.4434623122f},
		{0.9882950187f, -0.1525546312f},
		{-0.5424415469f, 0.8400935531f},
		{0.0392598175f, 0.9992290139f},
		{-0.9268565774f, 0.3754155636f},
		{-0.9741733670f, -0.2258012742f}
	};

	/* RE(Phi_Fract_SubQmf34[j]) = (float)cos(M_PI*f_center_34[j]*0.39); */
	/* IM(Phi_Fract_SubQmf34[j]) = (float)sin(M_PI*f_center_34[j]*0.39); */
	float[][] Phi_Fract_SubQmf34 = {
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{0.3387379348f, 0.9408807755f},
		{0.3387379348f, 0.9408807755f},
		{0.3387379348f, 0.9408807755f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{-0.7705132365f, 0.6374239922f},
		{-0.7705132365f, 0.6374239922f},
		{1.0000000000f, 0.0000000000f},
		{1.0000000000f, 0.0000000000f},
		{0.3387379348f, 0.9408807755f},
		{0.3387379348f, 0.9408807755f},
		{0.3387379348f, 0.9408807755f},
		{0.3387379348f, 0.9408807755f},
		{-0.7705132365f, 0.6374239922f},
		{-0.7705132365f, 0.6374239922f},
		{-0.8607420325f, -0.5090414286f},
		{0.3387379348f, 0.9408807755f},
		{0.1873813123f, -0.9822872281f},
		{-0.7705132365f, 0.6374239922f},
		{-0.8607420325f, -0.5090414286f},
		{-0.8607420325f, -0.5090414286f},
		{0.1873813123f, -0.9822872281f},
		{0.1873813123f, -0.9822872281f},
		{0.9876883626f, -0.1564344615f},
		{-0.8607420325f, -0.5090414286f}
	};

	/* RE(Q_Fract_allpass_Qmf[j][i]) = (float)cos(M_PI*(j+0.5)*(frac_delay_q[i])); */
	/* IM(Q_Fract_allpass_Qmf[j][i]) = (float)sin(M_PI*(j+0.5)*(frac_delay_q[i])); */
	float[][][] Q_Fract_allpass_Qmf = {
		{{0.7804303765f, 0.6252426505f}, {0.3826834261f, 0.9238795042f}, {0.8550928831f, 0.5184748173f}},
		{{-0.4399392009f, 0.8980275393f}, {-0.9238795042f, -0.3826834261f}, {-0.0643581524f, 0.9979268909f}},
		{{-0.9723699093f, -0.2334454209f}, {0.9238795042f, -0.3826834261f}, {-0.9146071672f, 0.4043435752f}},
		{{0.0157073960f, -0.9998766184f}, {-0.3826834261f, 0.9238795042f}, {-0.7814115286f, -0.6240159869f}},
		{{0.9792228341f, -0.2027871907f}, {-0.3826834261f, -0.9238795042f}, {0.1920081824f, -0.9813933372f}},
		{{0.4115142524f, 0.9114032984f}, {0.9238795042f, 0.3826834261f}, {0.9589683414f, -0.2835132182f}},
		{{-0.7996847630f, 0.6004201174f}, {-0.9238795042f, 0.3826834261f}, {0.6947838664f, 0.7192186117f}},
		{{-0.7604058385f, -0.6494481564f}, {0.3826834261f, -0.9238795042f}, {-0.3164770305f, 0.9486001730f}},
		{{0.4679299891f, -0.8837655187f}, {0.3826834261f, 0.9238795042f}, {-0.9874414206f, 0.1579856575f}},
		{{0.9645573497f, 0.2638732493f}, {-0.9238795042f, -0.3826834261f}, {-0.5966450572f, -0.8025052547f}},
		{{-0.0471066870f, 0.9988898635f}, {0.9238795042f, -0.3826834261f}, {0.4357025325f, -0.9000906944f}},
		{{-0.9851093888f, 0.1719288528f}, {-0.3826834261f, 0.9238795042f}, {0.9995546937f, -0.0298405960f}},
		{{-0.3826831877f, -0.9238796234f}, {-0.3826834261f, -0.9238795042f}, {0.4886211455f, 0.8724960685f}},
		{{0.8181498647f, -0.5750049949f}, {0.9238795042f, 0.3826834261f}, {-0.5477093458f, 0.8366686702f}},
		{{0.7396308780f, 0.6730127335f}, {-0.9238795042f, 0.3826834261f}, {-0.9951074123f, -0.0987988561f}},
		{{-0.4954589605f, 0.8686313629f}, {0.3826834261f, -0.9238795042f}, {-0.3725017905f, -0.9280315042f}},
		{{-0.9557929039f, -0.2940406799f}, {0.3826834261f, 0.9238795042f}, {0.6506417990f, -0.7593847513f}},
		{{0.0784594864f, -0.9969173074f}, {-0.9238795042f, -0.3826834261f}, {0.9741733670f, 0.2258014232f}},
		{{0.9900237322f, -0.1409008205f}, {0.9238795042f, -0.3826834261f}, {0.2502108514f, 0.9681913853f}},
		{{0.3534744382f, 0.9354441762f}, {-0.3826834261f, 0.9238795042f}, {-0.7427945137f, 0.6695194840f}},
		{{-0.8358076215f, 0.5490224361f}, {-0.3826834261f, -0.9238795042f}, {-0.9370992780f, -0.3490629196f}},
		{{-0.7181259394f, -0.6959131360f}, {0.9238795042f, 0.3826834261f}, {-0.1237744763f, -0.9923103452f}},
		{{0.5224990249f, -0.8526399136f}, {-0.9238795042f, 0.3826834261f}, {0.8226406574f, -0.5685616732f}},
		{{0.9460852146f, 0.3239179254f}, {0.3826834261f, -0.9238795042f}, {0.8844994903f, 0.4665412009f}},
		{{-0.1097348556f, 0.9939609170f}, {0.3826834261f, 0.9238795042f}, {-0.0047125919f, 0.9999889135f}},
		{{-0.9939610362f, 0.1097337380f}, {-0.9238795042f, -0.3826834261f}, {-0.8888573647f, 0.4581840038f}},
		{{-0.3239168525f, -0.9460855722f}, {0.9238795042f, -0.3826834261f}, {-0.8172453642f, -0.5762898922f}},
		{{0.8526405096f, -0.5224980116f}, {-0.3826834261f, 0.9238795042f}, {0.1331215799f, -0.9910997152f}},
		{{0.6959123611f, 0.7181267142f}, {-0.3826834261f, -0.9238795042f}, {0.9403476119f, -0.3402152061f}},
		{{-0.5490233898f, 0.8358070254f}, {0.9238795042f, 0.3826834261f}, {0.7364512086f, 0.6764906645f}},
		{{-0.9354437590f, -0.3534754813f}, {-0.9238795042f, 0.3826834261f}, {-0.2593250275f, 0.9657900929f}},
		{{0.1409019381f, -0.9900235534f}, {0.3826834261f, -0.9238795042f}, {-0.9762582779f, 0.2166097313f}},
		{{0.9969173670f, -0.0784583688f}, {0.3826834261f, 0.9238795042f}, {-0.6434556246f, -0.7654833794f}},
		{{0.2940396070f, 0.9557932615f}, {-0.9238795042f, -0.3826834261f}, {0.3812320232f, -0.9244794250f}},
		{{-0.8686318994f, 0.4954580069f}, {0.9238795042f, -0.3826834261f}, {0.9959943891f, -0.0894154981f}},
		{{-0.6730118990f, -0.7396316528f}, {-0.3826834261f, 0.9238795042f}, {0.5397993922f, 0.8417937160f}},
		{{0.5750059485f, -0.8181492686f}, {-0.3826834261f, -0.9238795042f}, {-0.4968227744f, 0.8678520322f}},
		{{0.9238792062f, 0.3826842010f}, {0.9238795042f, 0.3826834261f}, {-0.9992290139f, -0.0392601527f}},
		{{-0.1719299555f, 0.9851091504f}, {-0.9238795042f, 0.3826834261f}, {-0.4271997511f, -0.9041572809f}},
		{{-0.9988899231f, 0.0471055657f}, {0.3826834261f, -0.9238795042f}, {0.6041822433f, -0.7968461514f}},
		{{-0.2638721764f, -0.9645576477f}, {0.3826834261f, 0.9238795042f}, {0.9859085083f, 0.1672853529f}},
		{{0.8837660551f, -0.4679289758f}, {-0.9238795042f, -0.3826834261f}, {0.3075223565f, 0.9515408874f}},
		{{0.6494473219f, 0.7604066133f}, {0.9238795042f, -0.3826834261f}, {-0.7015317082f, 0.7126382589f}},
		{{-0.6004210114f, 0.7996840477f}, {-0.3826834261f, 0.9238795042f}, {-0.9562535882f, -0.2925389707f}},
		{{-0.9114028811f, -0.4115152657f}, {-0.3826834261f, -0.9238795042f}, {-0.1827499419f, -0.9831594229f}},
		{{0.2027882934f, -0.9792225957f}, {0.9238795042f, 0.3826834261f}, {0.7872582674f, -0.6166234016f}},
		{{0.9998766780f, -0.0157062728f}, {-0.9238795042f, 0.3826834261f}, {0.9107555747f, 0.4129458666f}},
		{{0.2334443331f, 0.9723701477f}, {0.3826834261f, -0.9238795042f}, {0.0549497530f, 0.9984891415f}},
		{{-0.8980280757f, 0.4399381876f}, {0.3826834261f, 0.9238795042f}, {-0.8599416018f, 0.5103924870f}},
		{{-0.6252418160f, -0.7804310918f}, {-0.9238795042f, -0.3826834261f}, {-0.8501682281f, -0.5265110731f}},
		{{0.6252435446f, -0.7804297209f}, {0.9238795042f, -0.3826834261f}, {0.0737608299f, -0.9972759485f}},
		{{0.8980270624f, 0.4399402142f}, {-0.3826834261f, 0.9238795042f}, {0.9183775187f, -0.3957053721f}},
		{{-0.2334465086f, 0.9723696709f}, {-0.3826834261f, -0.9238795042f}, {0.7754954696f, 0.6313531399f}},
		{{-0.9998766184f, -0.0157085191f}, {0.9238795042f, 0.3826834261f}, {-0.2012493610f, 0.9795400500f}},
		{{-0.2027861029f, -0.9792230725f}, {-0.9238795042f, 0.3826834261f}, {-0.9615978599f, 0.2744622827f}},
		{{0.9114037752f, -0.4115132093f}, {0.3826834261f, -0.9238795042f}, {-0.6879743338f, -0.7257350087f}},
		{{0.6004192233f, 0.7996854186f}, {0.3826834261f, 0.9238795042f}, {0.3254036009f, -0.9455752373f}},
		{{-0.6494490504f, 0.7604051232f}, {-0.9238795042f, -0.3826834261f}, {0.9888865948f, -0.1486719251f}},
		{{-0.8837650418f, -0.4679309726f}, {0.9238795042f, -0.3826834261f}, {0.5890548825f, 0.8080930114f}},
		{{0.2638743520f, -0.9645570517f}, {-0.3826834261f, 0.9238795042f}, {-0.4441666007f, 0.8959442377f}},
		{{0.9988898039f, 0.0471078083f}, {-0.3826834261f, -0.9238795042f}, {-0.9997915030f, 0.0204183888f}},
		{{0.1719277352f, 0.9851095676f}, {0.9238795042f, 0.3826834261f}, {-0.4803760946f, -0.8770626187f}},
		{{-0.9238800406f, 0.3826821446f}, {-0.9238795042f, 0.3826834261f}, {0.5555707216f, -0.8314692974f}},
		{{-0.5750041008f, -0.8181505203f}, {0.3826834261f, -0.9238795042f}, {0.9941320419f, 0.1081734300f}}
	};

	/* RE(Q_Fract_allpass_SubQmf20[j][i]) = (float)cos(M_PI*f_center_20[j]*frac_delay_q[i]); */
	/* IM(Q_Fract_allpass_SubQmf20[j][i]) = (float)sin(M_PI*f_center_20[j]*frac_delay_q[i]); */
	float[][][] Q_Fract_allpass_SubQmf20 = {
		{{0.9857769012f, 0.1680592746f}, {0.9569403529f, 0.2902846634f}, {0.9907300472f, 0.1358452588f}},
		{{0.8744080663f, 0.4851911962f}, {0.6343932748f, 0.7730104327f}, {0.9175986052f, 0.3975082636f}},
		{{0.6642524004f, 0.7475083470f}, {0.0980171412f, 0.9951847196f}, {0.7767338753f, 0.6298289299f}},
		{{0.3790524006f, 0.9253752232f}, {-0.4713967443f, 0.8819212914f}, {0.5785340071f, 0.8156582713f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{0.8744080663f, -0.4851911962f}, {0.6343932748f, -0.7730104327f}, {0.9175986052f, -0.3975082636f}},
		{{0.9857769012f, -0.1680592746f}, {0.9569403529f, -0.2902846634f}, {0.9907300472f, -0.1358452588f}},
		{{-0.7126385570f, 0.7015314102f}, {-0.5555702448f, -0.8314695954f}, {-0.3305967748f, 0.9437720776f}},
		{{-0.1175374240f, 0.9930684566f}, {-0.9807852507f, 0.1950903237f}, {0.2066311091f, 0.9784189463f}},
		{{-0.9947921634f, 0.1019244045f}, {0.5555702448f, -0.8314695954f}, {-0.7720130086f, 0.6356067061f}},
		{{-0.8400934935f, -0.5424416065f}, {0.9807852507f, 0.1950903237f}, {-0.9896889329f, 0.1432335079f}}
	};

	/* RE(Q_Fract_allpass_SubQmf34[j][i]) = (float)cos(M_PI*f_center_34[j]*frac_delay_q[i]); */
	/* IM(Q_Fract_allpass_SubQmf34[j][i]) = (float)sin(M_PI*f_center_34[j]*frac_delay_q[i]); */
	float[][][] Q_Fract_allpass_SubQmf34 = {
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{-0.9048270583f, 0.4257792532f}, {-0.0000000000f, -1.0000000000f}, {-0.5724321604f, 0.8199520707f}},
		{{-0.9048270583f, 0.4257792532f}, {-0.0000000000f, -1.0000000000f}, {-0.5724321604f, 0.8199520707f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}, {1.0000000000f, 0.0000000000f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{-0.9048270583f, 0.4257792532f}, {-0.0000000000f, -1.0000000000f}, {-0.5724321604f, 0.8199520707f}},
		{{-0.9048270583f, 0.4257792532f}, {-0.0000000000f, -1.0000000000f}, {-0.5724321604f, 0.8199520707f}},
		{{-0.6129069924f, -0.7901550531f}, {0.7071067691f, 0.7071067691f}, {-0.9917160273f, -0.1284494549f}},
		{{0.2181432247f, 0.9759167433f}, {-0.7071067691f, 0.7071067691f}, {0.4623677433f, 0.8866882324f}},
		{{0.6374240518f, -0.7705131769f}, {-1.0000000000f, 0.0000000000f}, {-0.3446428776f, -0.9387338758f}},
		{{-0.9048270583f, 0.4257792532f}, {-0.0000000000f, -1.0000000000f}, {-0.5724321604f, 0.8199520707f}},
		{{-0.6129069924f, -0.7901550531f}, {0.7071067691f, 0.7071067691f}, {-0.9917160273f, -0.1284494549f}},
		{{-0.6129069924f, -0.7901550531f}, {0.7071067691f, 0.7071067691f}, {-0.9917160273f, -0.1284494549f}},
		{{0.6374240518f, -0.7705131769f}, {-1.0000000000f, 0.0000000000f}, {-0.3446428776f, -0.9387338758f}},
		{{0.6374240518f, -0.7705131769f}, {-1.0000000000f, 0.0000000000f}, {-0.3446428776f, -0.9387338758f}},
		{{0.8910064697f, 0.4539906085f}, {0.7071067691f, -0.7071067691f}, {0.6730125546f, -0.7396310568f}},
		{{-0.6129069924f, -0.7901550531f}, {0.7071067691f, 0.7071067691f}, {-0.9917160273f, -0.1284494549f}}
	};

	float[] cos_alphas = {
		1.0000000000f, 0.9841239700f, 0.9594738210f,
		0.8946843079f, 0.8269340931f, 0.7071067812f,
		0.4533210856f, 0.0000000000f
	};

	float[] sin_alphas = {
		0.0000000000f, 0.1774824264f, 0.2817977763f,
		0.4466989918f, 0.5622988580f, 0.7071067812f,
		0.8913472911f, 1.0000000000f
	};

	float[][] cos_betas_normal = {
		{1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f},
		{1.0000000000f, 0.9995871699f, 0.9989419133f, 0.9972204583f, 0.9953790839f, 0.9920112747f, 0.9843408180f, 0.9681727381f},
		{1.0000000000f, 0.9984497744f, 0.9960279377f, 0.9895738413f, 0.9826814632f, 0.9701058164f, 0.9416098832f, 0.8822105900f},
		{1.0000000000f, 0.9959398908f, 0.9896038018f, 0.9727589768f, 0.9548355329f, 0.9223070404f, 0.8494349490f, 0.7013005535f},
		{1.0000000000f, 0.9932417400f, 0.9827071856f, 0.9547730996f, 0.9251668930f, 0.8717461589f, 0.7535520592f, 0.5198827312f},
		{1.0000000000f, 0.9902068095f, 0.9749613872f, 0.9346538534f, 0.8921231300f, 0.8158851259f, 0.6495964302f, 0.3313370772f},
		{1.0000000000f, 0.9880510933f, 0.9694670261f, 0.9204347876f, 0.8688622825f, 0.7768516704f, 0.5782161800f, 0.2069970356f},
		{1.0000000000f, 0.9858996945f, 0.9639898866f, 0.9063034786f, 0.8458214608f, 0.7384262300f, 0.5089811277f, 0.0905465944f}
	};

	float[][] sin_betas_normal = {
		{0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f},
		{0.0000000000f, -0.0287313368f, -0.0459897147f, -0.0745074328f, -0.0960233266f, -0.1261492408f, -0.1762757894f, -0.2502829383f},
		{0.0000000000f, -0.0556601118f, -0.0890412670f, -0.1440264301f, -0.1853028382f, -0.2426823129f, -0.3367058477f, -0.4708550466f},
		{0.0000000000f, -0.0900207420f, -0.1438204281f, -0.2318188366f, -0.2971348264f, -0.3864579191f, -0.5276933461f, -0.7128657193f},
		{0.0000000000f, -0.1160639735f, -0.1851663774f, -0.2973353800f, -0.3795605619f, -0.4899577884f, -0.6573882369f, -0.8542376401f},
		{0.0000000000f, -0.1396082894f, -0.2223742196f, -0.3555589603f, -0.4517923427f, -0.5782140273f, -0.7602792104f, -0.9435124489f},
		{0.0000000000f, -0.1541266914f, -0.2452217065f, -0.3908961522f, -0.4950538699f, -0.6296836366f, -0.8158836002f, -0.9783415698f},
		{0.0000000000f, -0.1673373610f, -0.2659389001f, -0.4226275012f, -0.5334660781f, -0.6743342664f, -0.8607776784f, -0.9958922202f}
	};

	float[][] cos_betas_fine = {
		{1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f, 1.0000000000f},
		{1.0000000000f, 0.9995871699f, 0.9989419133f, 0.9972204583f, 0.9953790839f, 0.9920112747f, 0.9843408180f, 0.9681727381f},
		{1.0000000000f, 0.9984497744f, 0.9960279377f, 0.9895738413f, 0.9826814632f, 0.9701058164f, 0.9416098832f, 0.8822105900f},
		{1.0000000000f, 0.9968361371f, 0.9918968104f, 0.9787540479f, 0.9647515190f, 0.9392903010f, 0.8820167114f, 0.7645325390f},
		{1.0000000000f, 0.9950262915f, 0.9872675041f, 0.9666584578f, 0.9447588606f, 0.9050918405f, 0.8165997379f, 0.6383824796f},
		{1.0000000000f, 0.9932417400f, 0.9827071856f, 0.9547730996f, 0.9251668930f, 0.8717461589f, 0.7535520592f, 0.5198827312f},
		{1.0000000000f, 0.9908827998f, 0.9766855904f, 0.9391249214f, 0.8994531782f, 0.8282352693f, 0.6723983174f, 0.3719473225f},
		{1.0000000000f, 0.9890240165f, 0.9719459866f, 0.9268448110f, 0.8793388536f, 0.7944023271f, 0.6101812098f, 0.2621501145f},
		{1.0000000000f, 0.9876350461f, 0.9684073447f, 0.9176973944f, 0.8643930070f, 0.7693796058f, 0.5646720713f, 0.1838899556f},
		{1.0000000000f, 0.9866247085f, 0.9658349704f, 0.9110590761f, 0.8535668048f, 0.7513165426f, 0.5320914819f, 0.1289530943f},
		{1.0000000000f, 0.9858996945f, 0.9639898866f, 0.9063034786f, 0.8458214608f, 0.7384262300f, 0.5089811277f, 0.0905465944f},
		{1.0000000000f, 0.9851245614f, 0.9620180268f, 0.9012265590f, 0.8375623272f, 0.7247108045f, 0.4845204297f, 0.0504115003f},
		{1.0000000000f, 0.9846869856f, 0.9609052357f, 0.8983639533f, 0.8329098386f, 0.7169983441f, 0.4708245354f, 0.0281732509f},
		{1.0000000000f, 0.9844406325f, 0.9602788522f, 0.8967533934f, 0.8302936455f, 0.7126658102f, 0.4631492839f, 0.0157851140f},
		{1.0000000000f, 0.9843020502f, 0.9599265269f, 0.8958477331f, 0.8288229094f, 0.7102315840f, 0.4588429315f, 0.0088578059f},
		{1.0000000000f, 0.9842241136f, 0.9597283916f, 0.8953385094f, 0.8279961409f, 0.7088635748f, 0.4564246834f, 0.0049751355f}
	};

	float[][] sin_betas_fine = {
		{0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f, 0.0000000000f},
		{0.0000000000f, -0.0287313368f, -0.0459897147f, -0.0745074328f, -0.0960233266f, -0.1261492408f, -0.1762757894f, -0.2502829383f},
		{0.0000000000f, -0.0556601118f, -0.0890412670f, -0.1440264301f, -0.1853028382f, -0.2426823129f, -0.3367058477f, -0.4708550466f},
		{0.0000000000f, -0.0794840594f, -0.1270461238f, -0.2050378347f, -0.2631625097f, -0.3431234916f, -0.4712181245f, -0.6445851354f},
		{0.0000000000f, -0.0996126459f, -0.1590687758f, -0.2560691819f, -0.3277662204f, -0.4252161335f, -0.5772043556f, -0.7697193058f},
		{0.0000000000f, -0.1160639735f, -0.1851663774f, -0.2973353800f, -0.3795605619f, -0.4899577884f, -0.6573882369f, -0.8542376401f},
		{0.0000000000f, -0.1347266752f, -0.2146747714f, -0.3435758752f, -0.4370171396f, -0.5603805303f, -0.7401895046f, -0.9282538388f},
		{0.0000000000f, -0.1477548470f, -0.2352041647f, -0.3754446647f, -0.4761965776f, -0.6073919186f, -0.7922618830f, -0.9650271071f},
		{0.0000000000f, -0.1567705832f, -0.2493736450f, -0.3972801182f, -0.5028167951f, -0.6387918458f, -0.8253153651f, -0.9829468369f},
		{0.0000000000f, -0.1630082348f, -0.2591578860f, -0.4122758299f, -0.5209834064f, -0.6599420072f, -0.8466868694f, -0.9916506943f},
		{0.0000000000f, -0.1673373610f, -0.2659389001f, -0.4226275012f, -0.5334660781f, -0.6743342664f, -0.8607776784f, -0.9958922202f},
		{0.0000000000f, -0.1718417832f, -0.2729859267f, -0.4333482310f, -0.5463417868f, -0.6890531546f, -0.8747799456f, -0.9987285320f},
		{0.0000000000f, -0.1743316967f, -0.2768774604f, -0.4392518725f, -0.5534087104f, -0.6970748701f, -0.8822268738f, -0.9996030552f},
		{0.0000000000f, -0.1757175038f, -0.2790421580f, -0.4425306221f, -0.5573261722f, -0.7015037013f, -0.8862802834f, -0.9998754073f},
		{0.0000000000f, -0.1764921355f, -0.2802517850f, -0.4443611583f, -0.5595110229f, -0.7039681080f, -0.8885173967f, -0.9999607689f},
		{0.0000000000f, -0.1769262394f, -0.2809295540f, -0.4453862969f, -0.5607337966f, -0.7053456119f, -0.8897620516f, -0.9999876239f}
	};

	float[][] sincos_alphas_B_normal = {
		{0.0561454100f, 0.0526385859f, 0.0472937334f, 0.0338410641f, 0.0207261065f, 0.0028205635f, 0.0028205635f, 0.0028205635f},
		{0.1249065138f, 0.1173697697f, 0.1057888284f, 0.0761985131f, 0.0468732723f, 0.0063956103f, 0.0063956103f, 0.0063956103f},
		{0.1956693050f, 0.1846090179f, 0.1673645109f, 0.1220621836f, 0.0757362479f, 0.0103882630f, 0.0103882630f, 0.0103882630f},
		{0.3015113269f, 0.2870525790f, 0.2637738799f, 0.1984573949f, 0.1260749909f, 0.0175600126f, 0.0175600126f, 0.0175600126f},
		{0.4078449476f, 0.3929852420f, 0.3680589270f, 0.2911029124f, 0.1934512363f, 0.0278686716f, 0.0278686716f, 0.0278686716f},
		{0.5336171261f, 0.5226637762f, 0.5033652606f, 0.4349162672f, 0.3224682122f, 0.0521999036f, 0.0521999036f, 0.0521999036f},
		{0.6219832023f, 0.6161847276f, 0.6057251063f, 0.5654342668f, 0.4826149915f, 0.1058044758f, 0.1058044758f, 0.1058044758f},
		{0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f},
		{0.7830305572f, 0.7876016373f, 0.7956739618f, 0.8247933372f, 0.8758325942f, 0.9943869542f, 0.9943869542f, 0.9943869542f},
		{0.8457261833f, 0.8525388778f, 0.8640737401f, 0.9004708933f, 0.9465802987f, 0.9986366532f, 0.9986366532f, 0.9986366532f},
		{0.9130511848f, 0.9195447612f, 0.9298024282f, 0.9566917233f, 0.9811098801f, 0.9996115928f, 0.9996115928f, 0.9996115928f},
		{0.9534625907f, 0.9579148236f, 0.9645845234f, 0.9801095128f, 0.9920207064f, 0.9998458099f, 0.9998458099f, 0.9998458099f},
		{0.9806699215f, 0.9828120260f, 0.9858950861f, 0.9925224431f, 0.9971278825f, 0.9999460406f, 0.9999460406f, 0.9999460406f},
		{0.9921685024f, 0.9930882705f, 0.9943886135f, 0.9970926648f, 0.9989008403f, 0.9999795479f, 0.9999795479f, 0.9999795479f},
		{0.9984226014f, 0.9986136287f, 0.9988810254f, 0.9994272242f, 0.9997851906f, 0.9999960221f, 0.9999960221f, 0.9999960221f}
	};

	float[][] sincos_alphas_B_fine = {
		{0.0031622158f, 0.0029630181f, 0.0026599892f, 0.0019002704f, 0.0011626042f, 0.0001580278f, 0.0001580278f, 0.0001580278f},
		{0.0056232673f, 0.0052689825f, 0.0047302825f, 0.0033791756f, 0.0020674015f, 0.0002811710f, 0.0002811710f, 0.0002811710f},
		{0.0099994225f, 0.0093696693f, 0.0084117414f, 0.0060093796f, 0.0036766009f, 0.0005000392f, 0.0005000392f, 0.0005000392f},
		{0.0177799194f, 0.0166607102f, 0.0149581377f, 0.0106875809f, 0.0065392545f, 0.0008893767f, 0.0008893767f, 0.0008893767f},
		{0.0316069684f, 0.0296211579f, 0.0265987295f, 0.0190113813f, 0.0116349973f, 0.0015826974f, 0.0015826974f, 0.0015826974f},
		{0.0561454100f, 0.0526385859f, 0.0472937334f, 0.0338410641f, 0.0207261065f, 0.0028205635f, 0.0028205635f, 0.0028205635f},
		{0.0791834041f, 0.0742798103f, 0.0667907269f, 0.0478705292f, 0.0293500747f, 0.0039966755f, 0.0039966755f, 0.0039966755f},
		{0.1115021177f, 0.1047141985f, 0.0943053154f, 0.0678120561f, 0.0416669150f, 0.0056813213f, 0.0056813213f, 0.0056813213f},
		{0.1565355066f, 0.1473258371f, 0.1330924027f, 0.0963282233f, 0.0594509113f, 0.0081277946f, 0.0081277946f, 0.0081277946f},
		{0.2184643682f, 0.2064579524f, 0.1876265439f, 0.1375744167f, 0.0856896681f, 0.0117817338f, 0.0117817338f, 0.0117817338f},
		{0.3015113269f, 0.2870525790f, 0.2637738799f, 0.1984573949f, 0.1260749909f, 0.0175600126f, 0.0175600126f, 0.0175600126f},
		{0.3698741335f, 0.3547727297f, 0.3298252076f, 0.2556265829f, 0.1665990017f, 0.0236344541f, 0.0236344541f, 0.0236344541f},
		{0.4480623975f, 0.4339410024f, 0.4098613774f, 0.3322709108f, 0.2266784729f, 0.0334094131f, 0.0334094131f, 0.0334094131f},
		{0.5336171261f, 0.5226637762f, 0.5033652606f, 0.4349162672f, 0.3224682122f, 0.0521999036f, 0.0521999036f, 0.0521999036f},
		{0.6219832023f, 0.6161847276f, 0.6057251063f, 0.5654342668f, 0.4826149915f, 0.1058044758f, 0.1058044758f, 0.1058044758f},
		{0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f, 0.7071067657f},
		{0.7830305572f, 0.7876016373f, 0.7956739618f, 0.8247933372f, 0.8758325942f, 0.9943869542f, 0.9943869542f, 0.9943869542f},
		{0.8457261833f, 0.8525388778f, 0.8640737401f, 0.9004708933f, 0.9465802987f, 0.9986366532f, 0.9986366532f, 0.9986366532f},
		{0.8940022267f, 0.9009412572f, 0.9121477564f, 0.9431839770f, 0.9739696219f, 0.9994417480f, 0.9994417480f, 0.9994417480f},
		{0.9290818561f, 0.9349525662f, 0.9440420138f, 0.9667755833f, 0.9860247275f, 0.9997206664f, 0.9997206664f, 0.9997206664f},
		{0.9534625907f, 0.9579148236f, 0.9645845234f, 0.9801095128f, 0.9920207064f, 0.9998458099f, 0.9998458099f, 0.9998458099f},
		{0.9758449068f, 0.9784554646f, 0.9822404252f, 0.9904914275f, 0.9963218730f, 0.9999305926f, 0.9999305926f, 0.9999305926f},
		{0.9876723320f, 0.9890880155f, 0.9911036356f, 0.9953496173f, 0.9982312259f, 0.9999669685f, 0.9999669685f, 0.9999669685f},
		{0.9937641889f, 0.9945023501f, 0.9955433130f, 0.9976981117f, 0.9991315558f, 0.9999838610f, 0.9999838610f, 0.9999838610f},
		{0.9968600642f, 0.9972374385f, 0.9977670024f, 0.9988535464f, 0.9995691924f, 0.9999920129f, 0.9999920129f, 0.9999920129f},
		{0.9984226014f, 0.9986136287f, 0.9988810254f, 0.9994272242f, 0.9997851906f, 0.9999960221f, 0.9999960221f, 0.9999960221f},
		{0.9995003746f, 0.9995611974f, 0.9996461891f, 0.9998192657f, 0.9999323103f, 0.9999987475f, 0.9999987475f, 0.9999987475f},
		{0.9998419236f, 0.9998611991f, 0.9998881193f, 0.9999428861f, 0.9999786185f, 0.9999996045f, 0.9999996045f, 0.9999996045f},
		{0.9999500038f, 0.9999561034f, 0.9999646206f, 0.9999819429f, 0.9999932409f, 0.9999998750f, 0.9999998750f, 0.9999998750f},
		{0.9999841890f, 0.9999861183f, 0.9999888121f, 0.9999942902f, 0.9999978628f, 0.9999999605f, 0.9999999605f, 0.9999999605f},
		{0.9999950000f, 0.9999956102f, 0.9999964621f, 0.9999981945f, 0.9999993242f, 0.9999999875f, 0.9999999875f, 0.9999999875f}
	};

	float[][] cos_gammas_normal = {
		{1.0000000000f, 0.9841239707f, 0.9594738226f, 0.8946843024f, 0.8269341029f, 0.7245688486f, 0.7245688486f, 0.7245688486f},
		{1.0000000000f, 0.9849690570f, 0.9617776789f, 0.9020941550f, 0.8436830391f, 0.7846832804f, 0.7846832804f, 0.7846832804f},
		{1.0000000000f, 0.9871656089f, 0.9676774734f, 0.9199102884f, 0.8785067015f, 0.8464232214f, 0.8464232214f, 0.8464232214f},
		{1.0000000000f, 0.9913533967f, 0.9786000177f, 0.9496063381f, 0.9277157252f, 0.9133354077f, 0.9133354077f, 0.9133354077f},
		{1.0000000000f, 0.9948924435f, 0.9875319180f, 0.9716329849f, 0.9604805241f, 0.9535949574f, 0.9535949574f, 0.9535949574f},
		{1.0000000000f, 0.9977406278f, 0.9945423840f, 0.9878736667f, 0.9833980494f, 0.9807207440f, 0.9807207440f, 0.9807207440f},
		{1.0000000000f, 0.9990607067f, 0.9977417734f, 0.9950323970f, 0.9932453273f, 0.9921884740f, 0.9921884740f, 0.9921884740f},
		{1.0000000000f, 0.9998081748f, 0.9995400312f, 0.9989936459f, 0.9986365356f, 0.9984265591f, 0.9984265591f, 0.9984265591f}
	};

	float[][] cos_gammas_fine = {
		{1.0000000000f, 0.9841239707f, 0.9594738226f, 0.8946843024f, 0.8269341029f, 0.7245688486f, 0.7245688486f, 0.7245688486f},
		{1.0000000000f, 0.9849690570f, 0.9617776789f, 0.9020941550f, 0.8436830391f, 0.7846832804f, 0.7846832804f, 0.7846832804f},
		{1.0000000000f, 0.9871656089f, 0.9676774734f, 0.9199102884f, 0.8785067015f, 0.8464232214f, 0.8464232214f, 0.8464232214f},
		{1.0000000000f, 0.9899597309f, 0.9750098690f, 0.9402333855f, 0.9129698759f, 0.8943765944f, 0.8943765944f, 0.8943765944f},
		{1.0000000000f, 0.9926607607f, 0.9819295710f, 0.9580160104f, 0.9404993670f, 0.9293004472f, 0.9293004472f, 0.9293004472f},
		{1.0000000000f, 0.9948924435f, 0.9875319180f, 0.9716329849f, 0.9604805241f, 0.9535949574f, 0.9535949574f, 0.9535949574f},
		{1.0000000000f, 0.9972074644f, 0.9932414270f, 0.9849197629f, 0.9792926592f, 0.9759092525f, 0.9759092525f, 0.9759092525f},
		{1.0000000000f, 0.9985361982f, 0.9964742028f, 0.9922136306f, 0.9893845420f, 0.9877041371f, 0.9877041371f, 0.9877041371f},
		{1.0000000000f, 0.9992494366f, 0.9981967170f, 0.9960386625f, 0.9946185834f, 0.9937800239f, 0.9937800239f, 0.9937800239f},
		{1.0000000000f, 0.9996194722f, 0.9990869422f, 0.9979996269f, 0.9972873651f, 0.9968679747f, 0.9968679747f, 0.9968679747f},
		{1.0000000000f, 0.9998081748f, 0.9995400312f, 0.9989936459f, 0.9986365356f, 0.9984265591f, 0.9984265591f, 0.9984265591f},
		{1.0000000000f, 0.9999390971f, 0.9998540271f, 0.9996809352f, 0.9995679735f, 0.9995016284f, 0.9995016284f, 0.9995016284f},
		{1.0000000000f, 0.9999807170f, 0.9999537862f, 0.9998990191f, 0.9998632947f, 0.9998423208f, 0.9998423208f, 0.9998423208f},
		{1.0000000000f, 0.9999938979f, 0.9999853814f, 0.9999680568f, 0.9999567596f, 0.9999501270f, 0.9999501270f, 0.9999501270f},
		{1.0000000000f, 0.9999980703f, 0.9999953731f, 0.9999898968f, 0.9999863277f, 0.9999842265f, 0.9999842265f, 0.9999842265f},
		{1.0000000000f, 0.9999993891f, 0.9999985397f, 0.9999968037f, 0.9999956786f, 0.9999950155f, 0.9999950155f, 0.9999950155f}
	};

	float[][] sin_gammas_normal = {
		{0.0000000000f, 0.1774824223f, 0.2817977711f, 0.4466990028f, 0.5622988435f, 0.6892024258f, 0.6892024258f, 0.6892024258f},
		{0.0000000000f, 0.1727308798f, 0.2738315110f, 0.4315392630f, 0.5368416242f, 0.6198968861f, 0.6198968861f, 0.6198968861f},
		{0.0000000000f, 0.1596999079f, 0.2521910140f, 0.3921288836f, 0.4777300236f, 0.5325107795f, 0.5325107795f, 0.5325107795f},
		{0.0000000000f, 0.1312190642f, 0.2057717310f, 0.3134450552f, 0.3732874674f, 0.4072080955f, 0.4072080955f, 0.4072080955f},
		{0.0000000000f, 0.1009407043f, 0.1574189028f, 0.2364938532f, 0.2783471983f, 0.3010924396f, 0.3010924396f, 0.3010924396f},
		{0.0000000000f, 0.0671836269f, 0.1043333428f, 0.1552598422f, 0.1814615013f, 0.1954144885f, 0.1954144885f, 0.1954144885f},
		{0.0000000000f, 0.0433324862f, 0.0671666110f, 0.0995516398f, 0.1160332699f, 0.1247478739f, 0.1247478739f, 0.1247478739f},
		{0.0000000000f, 0.0195860576f, 0.0303269852f, 0.0448519274f, 0.0522022017f, 0.0560750040f, 0.0560750040f, 0.0560750040f}
	};

	float[][] sin_gammas_fine = {
		{0.0000000000f, 0.1774824223f, 0.2817977711f, 0.4466990028f, 0.5622988435f, 0.6892024258f, 0.6892024258f, 0.6892024258f},
		{0.0000000000f, 0.1727308798f, 0.2738315110f, 0.4315392630f, 0.5368416242f, 0.6198968861f, 0.6198968861f, 0.6198968861f},
		{0.0000000000f, 0.1596999079f, 0.2521910140f, 0.3921288836f, 0.4777300236f, 0.5325107795f, 0.5325107795f, 0.5325107795f},
		{0.0000000000f, 0.1413496768f, 0.2221615526f, 0.3405307340f, 0.4080269669f, 0.4473147744f, 0.4473147744f, 0.4473147744f},
		{0.0000000000f, 0.1209322714f, 0.1892467110f, 0.2867147079f, 0.3397954394f, 0.3693246252f, 0.3693246252f, 0.3693246252f},
		{0.0000000000f, 0.1009407043f, 0.1574189028f, 0.2364938532f, 0.2783471983f, 0.3010924396f, 0.3010924396f, 0.3010924396f},
		{0.0000000000f, 0.0746811420f, 0.1160666523f, 0.1730117353f, 0.2024497161f, 0.2181768341f, 0.2181768341f, 0.2181768341f},
		{0.0000000000f, 0.0540875291f, 0.0838997203f, 0.1245476266f, 0.1453211203f, 0.1563346972f, 0.1563346972f, 0.1563346972f},
		{0.0000000000f, 0.0387371058f, 0.0600276114f, 0.0889212171f, 0.1036044086f, 0.1113609634f, 0.1113609634f, 0.1113609634f},
		{0.0000000000f, 0.0275846110f, 0.0427233177f, 0.0632198125f, 0.0736064637f, 0.0790837596f, 0.0790837596f, 0.0790837596f},
		{0.0000000000f, 0.0195860576f, 0.0303269852f, 0.0448519274f, 0.0522022017f, 0.0560750040f, 0.0560750040f, 0.0560750040f},
		{0.0000000000f, 0.0110363955f, 0.0170857974f, 0.0252592108f, 0.0293916021f, 0.0315673054f, 0.0315673054f, 0.0315673054f},
		{0.0000000000f, 0.0062101284f, 0.0096138203f, 0.0142109649f, 0.0165345659f, 0.0177576316f, 0.0177576316f, 0.0177576316f},
		{0.0000000000f, 0.0034934509f, 0.0054071189f, 0.0079928316f, 0.0092994041f, 0.0099871631f, 0.0099871631f, 0.0099871631f},
		{0.0000000000f, 0.0019645397f, 0.0030419905f, 0.0044951511f, 0.0052291853f, 0.0056166498f, 0.0056166498f, 0.0056166498f},
		{0.0000000000f, 0.0011053943f, 0.0017089869f, 0.0025283670f, 0.0029398552f, 0.0031573685f, 0.0031573685f, 0.0031573685f}
	};

	float[] sf_iid_normal = {
		1.4119827747f, 1.4031381607f, 1.3868767023f,
		1.3483997583f, 1.2912493944f, 1.1960374117f,
		1.1073724031f, 1.0000000000f, 0.8796171546f,
		0.7546485662f, 0.5767799020f, 0.4264014363f,
		0.2767182887f, 0.1766446233f, 0.0794016272f
	};

	float[] sf_iid_fine = {
		1.4142065048f, 1.4141912460f, 1.4141428471f,
		1.4139900208f, 1.4135069847f, 1.4119827747f,
		1.4097729921f, 1.4053947926f, 1.3967796564f,
		1.3800530434f, 1.3483997583f, 1.3139201403f,
		1.2643101215f, 1.1960374117f, 1.1073724031f,
		1.0000000000f, 0.8796171546f, 0.7546485662f,
		0.6336560845f, 0.5230810642f, 0.4264014363f,
		0.3089554012f, 0.2213746458f, 0.1576878875f,
		0.1119822487f, 0.0794016272f, 0.0446990170f,
		0.0251446925f, 0.0141414283f, 0.0079525812f,
		0.0044721137f
	};
	float[] ipdopd_cos_tab = {
		1.000000000000000f,
		0.707106781186548f,
		0.000000000000000f,
		-0.707106781186547f,
		-1.000000000000000f,
		-0.707106781186548f,
		-0.000000000000000f,
		0.707106781186547f,
		1.000000000000000f
	};

	float[] ipdopd_sin_tab = {
		0.000000000000000f,
		0.707106781186547f,
		1.000000000000000f,
		0.707106781186548f,
		0.000000000000000f,
		-0.707106781186547f,
		-1.000000000000000f,
		-0.707106781186548f,
		-0.000000000000000f
	};
}
