#include "ofApp.h"

void ofApp::setup() {
	ofSetBackgroundColor(ofColor::black);
	ofSetCircleResolution(30);

	tracker.setup();
}

void ofApp::update() {
	tracker.update();
	if (tracker.hasBlobs()) {
		ofPoint blobPos = tracker.getCentroid();
		int scaledX = blobPos.x * ofGetWidth() / GRABBER_WIDTH;
		int scaledY = blobPos.y * ofGetHeight() / GRABBER_HEIGHT;
		paddleY = blobPos.y;
		//Hoe zet ik een waarde dat hij zowel de boven als onderkant kan bereiken?
	}

	//paddleY = MIN(MAX(mouseY - PADDLE_HEIGHT / 2, 0), ofGetHeight() - PADDLE_HEIGHT);
	bool wasReset = ball.update(paddleY);
	if (wasReset) {
		missedCount++;
	}
}

void ofApp::draw() {
	ball.draw();
	
	if (tracker.hasBlobs()) {
		
	}
	ofDrawRectangle(ofGetWidth() - PADDLE_WIDTH,
		paddleY,
		PADDLE_WIDTH, PADDLE_HEIGHT);

	ofDrawBitmapString("Missed: " + ofToString(missedCount), 10, 10);

	tracker.draw();

	
}

void ofApp::keyPressed(int key) {
	ball.reset();
	
	if (key == 's') {
		tracker.toggleSetup();
	}
	else if (key == 'b') {
		tracker.toggleBlobs();
	}
}
void ofApp::mousePressed(int x, int y, int button) {
	if (button == OF_MOUSE_BUTTON_RIGHT) {
		tracker.selectColor(x, y);
	}
}
