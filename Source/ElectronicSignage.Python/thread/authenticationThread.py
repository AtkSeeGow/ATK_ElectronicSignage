from controller.authenticationController import AuthenticationController
from PyQt5 import QtCore
from PyQt5.QtWidgets import *


class AuthenticationThread(QtCore.QThread):
    def __init__(self, cntrololer: AuthenticationController, parent=None):
        super().__init__(parent)
        self.cntrololer = cntrololer

    def run(self):
        self.cntrololer.generateToken()
