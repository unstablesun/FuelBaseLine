#!/usr/bin/python

import sys
import shutil
import os
import fileinput
import time


if len(sys.argv) < 6:
    print 'arg 1 = Target Build Sku'
    print 'arg 2 = Unity folder name'
    print 'arg 3 = Output File Name'
    print 'arg 4 = Unity Project Path'
    print 'arg 5 = Build Number'
    print 'arg 6 = Target Debug Environment (internal | sandbox | production | none)'
    print 'Example:'
    print 'python buildTarget.py Android Unity SeaLegs_v1.apk SeaLegsProjectPath 4.3.2 none'
    sys.exit(1)


#This system only works with Unity 5.x.x - 4.x.x does not permit building from the command line
targetBuildSku = sys.argv[1] 	    #build target - iOS, Android
unityFolderName = sys.argv[2] 	    #name of the unity folder in applications
outputFileName = sys.argv[3] 	    #name of the output file
unityProjectPath = sys.argv[4] 	    #this is our project that we are building from
buildNumber = sys.argv[5]
targetDebugEnv = sys.argv[6]


print 'Target Build Sku : ' + targetBuildSku
print 'Unity Folder Name : ' + unityFolderName
print 'Output File Name : ' + outputFileName
print 'Unity Project Path : ' + unityProjectPath

if targetBuildSku == 'Android':
    keystorePath = sys.argv[7]
    keystorePass = sys.argv[8]
    keystoreAlias = sys.argv[9]
    keystoreAliasPass = sys.argv[10]

    print 'building Android...'
    os.system("\"/Applications/" + unityFolderName + "/Unity.app/Contents/MacOS/Unity\" -quit -batchmode -buildTarget android -logFile /dev/stdout -projectPath \"" + unityProjectPath + "\" -executeMethod AutoBuilder.PerformAndroidBuild \"" + outputFileName + "\" " + buildNumber + " " + targetDebugEnv + " \"" + keystorePath + "\" " + keystorePass + " \"" + keystoreAlias + "\" " + keystoreAliasPass)
    print 'Build Complete'
elif targetBuildSku == 'iOS':
    print 'building iOS...'
    os.system("\"/Applications/" + unityFolderName + "/Unity.app/Contents/MacOS/Unity\" -quit -batchmode -buildTarget ios -logFile /dev/stdout -projectPath \"" + unityProjectPath + "\" -executeMethod AutoBuilder.PerformiOSBuild \"" + outputFileName + "\" " + buildNumber + " " + targetDebugEnv)
    print 'Build Complete'
else:
    print targetBuildSku + ' not defined'


print 'cleaning up...'
print 'done.'

