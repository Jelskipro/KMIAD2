#pragma once

#include "ofMain.h"
#include "ofxOpenCV.h"

#define GRABBER_WIDTH 320
#define GRABBER_HEIGHT 240
#define HUE_MARGIN 5
#define MIN_SIZE 50

class ofApp : public ofBaseApp{

	public:
		void setup();
		void update();
		void draw();

		void keyPressed(int key);
		void mousePressed(int x, int y, int button);
		
		ofVideoGrabber grabber;
		
		ofxCvColorImage rgbImage;
		ofxCvColorImage hsvImage;

		ofxCvGrayscaleImage hue;
		ofxCvGrayscaleImage saturation;
		ofxCvGrayscaleImage value; // = brightness

		ofxCvGrayscaleImage filtered1;
		ofxCvGrayscaleImage filtered2;

		ofxCvContourFinder contours1;
		ofxCvContourFinder contours2;

		bool showHSVComponents = false;
		bool showVideo = true;
		bool showFiltered = true;
		bool showContours = true;

		int findHue1;
		int findHue2;

};
