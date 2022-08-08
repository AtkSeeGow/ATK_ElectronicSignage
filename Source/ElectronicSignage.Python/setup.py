#!python
#cython: language_level=3

from thread.authenticationThread import AuthenticationThread
from controller.tableController import TableController
from thread.controllerThread import ControllerThread
from controller.heartbeatController import HeartbeatController
from controller.toDoController import ToDoController
from controller.transportController import TransportController
from controller.authenticationController import AuthenticationController
from config import Config

import sys

from datetime import datetime
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *

from view.mainWindow import Ui_MainWindow


class Window(QMainWindow, Ui_MainWindow):
    def __init__(self, parent=None):
        super().__init__(parent)
        self.setupUi(self)

        self.config = Config()

        self.authenticationController = AuthenticationController(self.config)
        self.authenticationThread = AuthenticationThread(self.authenticationController)

        self.tableController = TableController(self.config)

        self.transportController = TransportController(self.config, self.tableController, self.transportTableWidget)
        self.transportThread = ControllerThread(self.transportController)
        self.transportThread.finished.connect(self.transportController.display)

        self.toDoController = ToDoController(self.config, self.tableController, self.toDoTableWidget)
        self.toDoThread = ControllerThread(self.toDoController)
        self.toDoThread.finished.connect(self.toDoController.display)

        self.heartbeatController = HeartbeatController(self.config, self.tableController, self.heartbeatTableWidget)
        self.heartbeatThread = ControllerThread(self.heartbeatController)
        self.heartbeatThread.finished.connect(self.heartbeatController.display)

        self.authenticationController.generateToken()
        self.toDoController.get()
        self.toDoController.display()
        self.heartbeatController.get()
        self.heartbeatController.display()

        timer = QTimer(self)
        timer.setInterval(1000)
        timer.timeout.connect(self.secondEvent)
        timer.start()

        timer = QTimer(self)
        timer.setInterval(60000)
        timer.timeout.connect(self.minuteEvent)
        timer.start()

        timer = QTimer(self)
        timer.setInterval(1800000)
        timer.timeout.connect(self.halfHourEvent)
        timer.start()

        self.showFullScreen()

    def secondEvent(self):
        self.datetimeLabel.setText(
            datetime.now().strftime('%Y/%m/%d %H:%M:%S'))
        self.transportThread.start()

    def minuteEvent(self):
        self.toDoThread.start()
        self.heartbeatThread.start()

    def halfHourEvent(self):
        self.authenticationController.generateToken()


if __name__ == "__main__":
    app = QApplication(sys.argv)
    win = Window()
    win.show()
    sys.exit(app.exec())
