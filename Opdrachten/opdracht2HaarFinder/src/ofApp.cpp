#include "ofApp.h"

void ofApp::setup(){
	grabber.setup(640, 480);
	haarFinder.setup("haarcascade_fullbody.xml");

	image.load("eyes.png");

}

void ofApp::update(){
	grabber.update();
	if (grabber.isFrameNew()) {
		haarFinder.findHaarObjects(grabber.getPixels(), 100, 100);

	}
}

void ofApp::draw(){
	grabber.draw(0, 0);

	ofSetColor(ofColor::white);
	for (int i = 0; i < haarFinder.blobs.size(); i++) {
		ofRectangle boundingBox = haarFinder.blobs[i].boundingRect;
		if (drawBox) {
			ofDrawRectangle(boundingBox);
			//ofDrawRectangle(haarFinder.blobs[i].boundingRect);
		}
		if (drawImage) {
			image.draw(boundingBox);
		}
	}
}

void ofApp::keyPressed(int key){
	if (key == 'b') {
		drawBox = !drawBox;
	}
}

