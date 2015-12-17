#import "fuel.h"

extern "C"
{
    @interface fuel (Private)

+ (void)useDebugServers:(NSString*)sdkURL fuelAPIURL:(NSString*)fuelAPIURL tournamentAPIURL:(NSString*)tournamentAPIURL challengeAPIURL:(NSString*)challengeAPIURL cdnAPIURL:(NSString*)cdnAPIURL transactionAPIURL:(NSString*)transactionAPIURL fuelV2APIURL:(NSString*)fuelV2APIURL;

@end
    
    void iOSUseDebugServers(const char* sdkHost, const char* apiHost, const char* tournamentHost, const char* challengeHost, const char* cdnHost, const char* transactionHost, const char* fuelV2Host)
    {
        NSString* sdkURL = [NSString stringWithFormat:@"%s", sdkHost];
        NSString* fuelAPIURL = [NSString stringWithFormat:@"%s", apiHost];
        NSString* tournamentAPIURL = [NSString stringWithFormat:@"%s", tournamentHost];
        NSString* challengeAPIURL = [NSString stringWithFormat:@"%s", challengeHost];
        NSString* cdnAPIURL = [NSString stringWithFormat:@"%s", cdnHost];
        NSString* transactionAPIURL = [NSString stringWithFormat:@"%s", transactionHost];
        NSString* fuelV2APIURL = [NSString stringWithFormat:@"%s", fuelV2Host];
        
        [fuel useDebugServers:sdkURL
                   fuelAPIURL:fuelAPIURL
             tournamentAPIURL:tournamentAPIURL
              challengeAPIURL:challengeAPIURL
                    cdnAPIURL:cdnAPIURL
            transactionAPIURL:transactionAPIURL
                 fuelV2APIURL:fuelV2APIURL];
    }
    
}