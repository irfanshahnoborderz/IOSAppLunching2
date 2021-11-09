//  UnityPluginTest-1.mm
//  Created by OJ on 7/13/16.
//  In unity, You'd place this file in your "Assets>plugins>ios" folder
 
//Objective-C Code
#import <Foundation/Foundation.h>
 
  @interface SampleClass:NSObject
  /* method declaration */
- (int)isYelpInstalledX;
- (int)isFBInstalledX;
  @end
 
  @implementation SampleClass
 
- (int)isYelpInstalledX
   {
       int param = 0;
 
       if ([[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:@"yelp:"]])
       {  
           param = 1; //Installed
       }else{
           param = 0; //Not Installed
       }
 
       return param;
    }
 
- (int)isFBInstalledX
   {
       int param = 0;
 
        if ([[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:@"fb:"]])
        {
            param = 1; //Installed
        }else{
            param = 0; //Not Installed
        }
        return param;
    }
 
  @end
 
//C-wrapper that Unity communicates with
  extern "C"
  {
      int isFBInstalled()
     {
        SampleClass *status = [[SampleClass alloc] init];
        return [status isFBInstalledX];
     }
 
      int isYelpInstalled()
    {
        SampleClass *status = [[SampleClass alloc] init];
        return [status isYelpInstalledX];
    }
  }