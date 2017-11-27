#include "ColorTracker.h"

ColorTracker::ColorTracker() {}

void ColorTracker::setup() {
	grabber.setup(GRABBER_WIDTH, GRABBER_HEIGHT, true);
	grabberImage.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	hue.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
	filteredImage.allocate(GRABBER_WIDTH, GRABBER_HEIGHT);
}

void ColorTracker::update() {
	grabber.update();
	if (grabber.isFrameNew()) {
		grabberImage = grabber.getPixels();
		grabberImage.mirror(false, true);
		grabberImage.convertRgbToHsv();
		grabberImage.convertToGrayscalePlanarImage(hue, 0); // only interested in hue

		for (int i = 0; i < GRABBER_WIDTH * GRABBER_HEIGHT; i++) {
			if (ofInRange(hue.getPixels()[i], selectedHue - HUE_MARGIN, selectedHue + HUE_MARGIN)) {
				filteredImage.getPixels()[i] = 255;
			}
			else {
				filteredImage.getPixels()[i] = 0;
			}
		}
		filteredImage.flagImageChanged();

		contours.findContours(filteredImage, 50, GRABBER_WIDTH * GRABBER_HEIGHT / 2, 1, false);
	}
}

void ColorTracker::draw() {
	ofSetColor(ofColor::white);
	if (showSetup) {
		grabberImage.draw(0, 0);
		filteredImage.draw(0, 240);
	}

	if (showBlobs) {
		contours.draw(0, 0, ofGetWidth(), ofGetHeight());
		ofSetColor(ofColor::blue, 100);
		ofFill();
		for (auto& blob : contours.blobs) {
			ofDrawCircle(blob.centroid.x * ofGetWidth() / GRABBER_WIDTH,
				blob.centroid.y * ofGetHeight() / GRABBER_HEIGHT,
				20);
		}
		ofSetColor(ofColor::white);
	}
}

ofPoint ColorTracker::getCentroid() {
	if (hasBlobs()) {
		return contours.blobs[0].centroid;
	}
}

void ColorTracker::selectColor(int x, int y) {
	selectedHue = hue.getPixels()[y * GRABBER_WIDTH + x];
}

void ColorTracker::toggleSetup() {
	showSetup = !showSetup;
}

void ColorTracker::toggleBlobs() {
	showBlobs = !showBlobs;
}

bool ColorTracker::hasBlobs() {
	cout << selectedHue << endl;
	return selectedHue != -1 && contours.blobs.size() > 0;
}
