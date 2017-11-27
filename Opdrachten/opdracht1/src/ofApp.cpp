#include "ofApp.h"

void ofApp::setup(){
	grabber.setup(GRABBER_WIDTH, GRABBER_HEIGHT, true);
	
	rgbImage.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	hsvImage.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	
	hue.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	saturation.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	value.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	
	grabber.listDevices();
	grabber.setDeviceID(0);

	filtered1.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	filtered2.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);

}

void ofApp::update(){
	grabber.update();
	if (grabber.isFrameNew()) {
		rgbImage.setFromPixels(grabber.getPixels().getData(), 
		GRABBER_WIDTH, GRABBER_HEIGHT);

		rgbImage.mirror(false, true);
		hsvImage = rgbImage;
		hsvImage.convertRgbToHsv();

		hsvImage.convertToGrayscalePlanarImages(hue, saturation, value);
		for (int i = 0; i < GRABBER_WIDTH*GRABBER_HEIGHT; i++) {
			if (ofInRange(hue.getPixels()[i], findHue1 - HUE_MARGIN, findHue1 + HUE_MARGIN)) {
				filtered1.getPixels()[i] = 255;
			}
			else {
				filtered1.getPixels()[i] = 0;
			}
		}
		filtered1.flagImageChanged();
		contours1.findContours(filtered1, MIN_SIZE, GRABBER_WIDTH * GRABBER_HEIGHT / 2, 1, false);

		for (int i = 0; i < GRABBER_WIDTH*GRABBER_HEIGHT; i++) {
			if (ofInRange(hue.getPixels()[i], findHue2 - HUE_MARGIN, findHue2 + HUE_MARGIN)) {
				filtered2.getPixels()[i] = 255;
			}
			else {
				filtered2.getPixels()[i] = 0;
			}
		}
		filtered2.flagImageChanged();
		contours2.findContours(filtered2, MIN_SIZE, GRABBER_WIDTH * GRABBER_HEIGHT / 2, 1, false);
	}
}

void ofApp::draw(){
	if (showVideo) {
		rgbImage.draw(0, 0, ofGetWidth(), ofGetHeight());
	}
	if (showHSVComponents) {
		hsvImage.draw(0, 0);
		hue.draw(0, 240);
		saturation.draw(320, 240);
		value.draw(640, 240);
	}
	if (showFiltered) {
		filtered1.draw(0, 480);
		filtered2.draw(0, 480);

	}
	if (showContours) {
		contours1.draw(0, 0, ofGetWidth(), ofGetHeight());
		contours2.draw(0, 0, ofGetWidth(), ofGetHeight());
	}

	ofSetColor(ofColor::blue, 100);
	ofFill();
	for (int i = 0; i < contours1.blobs.size(); i++) {
		ofDrawCircle(contours1.blobs[i].centroid.x * ofGetWidth() / GRABBER_WIDTH,
			contours1.blobs[i].centroid.y * ofGetHeight() / GRABBER_HEIGHT,
			20);
	}
	for (int i = 0; i < contours2.blobs.size(); i++) {
		ofDrawCircle(contours2.blobs[i].centroid.x * ofGetWidth() / GRABBER_WIDTH,
			contours2.blobs[i].centroid.y * ofGetHeight() / GRABBER_HEIGHT,
			20);
	}
	ofSetColor(ofColor::white);
	}

void ofApp::keyPressed(int key){
	if (key == 'h') {
		showHSVComponents = !showHSVComponents;
	}
	else if (key == 'v') {
		showVideo = !showVideo;
	}
	else if (key == 'c') {
		showContours = !showContours;
	}
	else if (key == 'f') {
		showFiltered = !showFiltered;
	}
}

void ofApp::mousePressed(int x, int y, int button){
	 findHue1 = hue.getPixels()[y * GRABBER_WIDTH + x];
	cout << "Selected: " << findHue1 << endl;
	findHue2 = hue.getPixels()[y * GRABBER_WIDTH + x];
	cout << "Selected: " << findHue2 << endl;
}
