// Generated by Hybridizer version 1.2.10616
 #include "cuda_runtime.h"                                                                                  
 #include "device_launch_parameters.h"                                                                      
                                                                                                              
 #if defined(__CUDACC__)                                                                                      
 	#ifndef hyb_device                                                                                       
 		#define hyb_inline __forceinline__                                                                   
 		                                                                                                     
 		#define hyb_constant __constant__                                                                    
 		#if defined(HYBRIDIZER_NO_HOST)                                                                      
 			#define hyb_host                                                                                 
 			#define	hyb_device  __device__                                                                   
 		#else                                                                                                
 			#define hyb_host __host__                                                                        
 			#define	hyb_device  __device__                                                                   
 		#endif                                                                                               
 	#endif                                                                                                   
 #else                                                                                                        
 	#ifndef hyb_device                                                                                       
 		#define hyb_inline inline                                                                            
 		#define hyb_device                                                                                   
 		#define hyb_constant                                                                                 
 	#endif                                                                                                   
 #endif                                                                                                       
                                                                                                              
                                                                                                  
 #if defined _WIN32 || defined _WIN64 || defined __CYGWIN__                                                   
   #define BUILDING_DLL                                                                                       
   #ifdef BUILDING_DLL                                                                                        
     #ifdef __GNUC__                                                                                          
       #define DLL_PUBLIC __attribute__ ((dllexport))                                                         
     #else                                                                                                    
       #define DLL_PUBLIC __declspec(dllexport) // Note: actually gcc seems to also supports this syntax.     
     #endif                                                                                                   
   #else                                                                                                      
     #ifdef __GNUC__                                                                                          
       #define DLL_PUBLIC __attribute__ ((dllimport))                                                         
     #else                                                                                                    
       #define DLL_PUBLIC __declspec(dllimport) // Note: actually gcc seems to also supports this syntax.     
     #endif                                                                                                   
   #endif                                                                                                     
   #define DLL_LOCAL                                                                                          
 #else                                                                                                        
   #if __GNUC__ >= 4                                                                                          
     #define DLL_PUBLIC __attribute__ ((visibility ("default")))                                            
     #define DLL_LOCAL  __attribute__ ((visibility ("hidden")))                                             
   #else                                                                                                      
     #define DLL_PUBLIC                                                                                       
     #define DLL_LOCAL                                                                                        
   #endif                                                                                                     
 #endif                                                                                                       


#if CUDART_VERSION >= 9000
#include <cooperative_groups.h>
#endif
// hybridizer core types
#include <cstdint>
namespace hybridizer { struct hybridobject ; }
namespace hybridizer { struct runtime ; }

#pragma region defined enums and types
#if defined(__cplusplus) || defined(__CUDACC__)
namespace TryNumber2000 { 
struct TheHack___c ;
} // Leaving namespace
namespace TryNumber2000 { 
struct TheHack ;
} // Leaving namespace
// Intrinsic type Action`1 used
#define __TYPE_DECL_hybridizer_action__int____
namespace System { namespace Runtime { namespace Serialization { 
struct ISerializable ;
} } } // Leaving namespace
// Intrinsic type Exception used
#define __TYPE_DECL_hybridizer_baseexception___
namespace System { 
struct SystemException ;
} // Leaving namespace
namespace System { 
struct ArgumentException ;
} // Leaving namespace
namespace System { 
struct ArgumentOutOfRangeException ;
} // Leaving namespace
#endif
#pragma endregion

extern "C" void* __hybridizer_init_basic_runtime();
#include <cstdio>
// generating GetTypeID function
#include <cstring> // for strcmp
extern "C" DLL_PUBLIC int HybridizerGetTypeID( const char* fullTypeName)
{
	if (strcmp (fullTypeName, "TryNumber2000.TheHack") == 0) return 1000000 ; 
	return 0 ;
}
extern "C" DLL_PUBLIC const char* HybridizerGetTypeFromID( const int typeId)
{
	if (typeId == 1000000) return "TryNumber2000.TheHack" ; 
	return "" ;
}
extern "C" DLL_PUBLIC int HybridizerGetShallowSize (const char* fullTypeName) 
{
	#ifdef __TYPE_DECL__TryNumber2000_TheHack___
	if (strcmp (fullTypeName, "TryNumber2000.TheHack") == 0) return 8 ; 
	#endif
	return 0 ;
}

// Get various Hybridizer properties at runtime
struct __hybridizer_properties {
    int32_t UseHybridArrays;
    int32_t Flavor;
    int32_t CompatibilityMode;
    int32_t DelegateSupport;
    int32_t _dummy;
};
extern "C" DLL_PUBLIC __hybridizer_properties __HybridizerGetProperties () {
    __hybridizer_properties res;
    res.UseHybridArrays = 0;
    res.Flavor = 1;
    res.DelegateSupport = 0;
    res.CompatibilityMode = 0;
    return res ;
}
#include <cuda.h>                                     
 struct HybridModule                                  
 {                                                    
     void* module_data ;                              
     CUmodule module ;                                
 } ;                                                  
                                                      
 extern char __hybridizer_cubin_module_data [] ;      
 static HybridModule __hybridizer__gs_module = { 0 }; 
 static int __hybridizer_initialized = 0; 


// error code translation from CUresult to cudaError_t

namespace hybridizer {

	cudaError_t translateCUresult(int cures)
	{
		switch (cures)

		{
			case CUDA_SUCCESS: return cudaSuccess ;
			case CUDA_ERROR_INVALID_VALUE: return cudaErrorInvalidValue ;
			case CUDA_ERROR_LAUNCH_FAILED: return cudaErrorLaunchFailure ;
			case CUDA_ERROR_NOT_SUPPORTED: return cudaErrorNotSupported ;
			case CUDA_ERROR_ILLEGAL_INSTRUCTION : return cudaErrorLaunchFailure ;
			default: return cudaErrorUnknown ;
		}
	}

} // namespace hybridizer
#pragma region Wrappers definitions


#pragma endregion