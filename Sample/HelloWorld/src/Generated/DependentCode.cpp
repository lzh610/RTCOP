#include "./Generated/DependentCode.h"
#include "RTCOP/Core/LayerdObject.h"

#include "./Generated/BaseLayer.h"
#include "./Generated/English.h"
#include "./Generated/Japanese.h"


namespace RTCOP {
namespace Generated {
namespace DependentCode {

volatile void* GetLayerdObjectFinalizer(::English::Hello* obj)
{
	volatile void* vfp = 0;
	asm volatile("movq $_ZN7English5Hello27_RTCOP_FinalizePartialClassEv, %0" : "=r"(vfp) : : "memory");
	return vfp;
}

volatile void* GetLayerdObjectFinalizer(::Japanese::Hello* obj)
{
	volatile void* vfp = 0;
	asm volatile("movq $_ZN8Japanese5Hello27_RTCOP_FinalizePartialClassEv, %0" : "=r"(vfp) : : "memory");
	return vfp;
}


namespace baselayer { namespace Hello { 
volatile void** GetVirtualFunctionTable(BaseLayer* layer)
{
	volatile void** vft = 0;
	asm volatile("movq $_ZTVN5RTCOP4Core12LayerdObjectIN9baselayer5HelloEEE+16, %0" : "=r"(vft) : : "memory");
	return vft;
}

volatile void** GetVirtualFunctionTable(English* layer)
{
	volatile void** vft = 0;
	asm volatile("movq $_ZTVN7English5HelloE+16, %0" : "=r"(vft) : : "memory");
	return vft;
}

volatile void** GetVirtualFunctionTable(Japanese* layer)
{
	volatile void** vft = 0;
	asm volatile("movq $_ZTVN8Japanese5HelloE+16, %0" : "=r"(vft) : : "memory");
	return vft;
}

void ExecuteProceed_Print(void* layerdObject, volatile void* proceed)
{
	void(*pProceed)(void*) = (void(*)(void*))proceed;
	pProceed(layerdObject);
}

}}

} // namespace DependentCode {}
} // namespace Generated {}
} // namespace RTCOP {}
