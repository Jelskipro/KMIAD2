#pragma once

#include "ofMain.h"
#include "ofxOpenCv.h"

#define GRABBER_WIDTH 320
#define GRABBER_HEIGHT 240
#define HUE_MARGIN 5

class ColorTracker
{
public:
	ColorTracker();
	void setup();
	void update();
	void draw();
	void selectColor(int x, int y);
	ofPoint getCentroid();
	void toggleSetup();
	void toggleBlobs();
	bool hasBlobs();

private:
	ofVideoGrabber grabber;

	ofxCvColorImage grabberImage;
	ofxCvGrayscaleImage hue;
	ofxCvGrayscaleImage filteredImage;

	ofxCvContourFinder contours;

	bool showSetup = false;
	bool showBlobs = false;
	int selectedHue = -1;

};

