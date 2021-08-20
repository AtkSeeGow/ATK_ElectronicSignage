from PyQt5 import QtCore
from PyQt5.QtWidgets import *


class ControllerThread(QtCore.QThread):
    def __init__(self, cntrololer, parent=None):
        super().__init__(parent)
        self.cntrololer = cntrololer

    def run(self):
        self.cntrololer.get();
