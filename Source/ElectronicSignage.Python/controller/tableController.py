from config import Config
from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtWidgets import *


class TableController(object):
    def __init__(self, config: Config):
        self.config = config
    
    def getWhiteFonrColor(self):
        return 'white';

    def getRedFonrColor(self):
        return 'red'; 

    def getItem(self, text, pointSize, fontColor: str):
        item = QtWidgets.QTableWidgetItem()
        item.setText(text)
        item.setTextAlignment(QtCore.Qt.AlignCenter)
        font = QtGui.QFont()
        font.setFamily(self.config.fontFamily)
        font.setPointSize(pointSize)
        item.setFont(font)

        if fontColor == self.getWhiteFonrColor:
            brush = QtGui.QBrush(QtGui.QColor(255, 255, 255))
            brush.setStyle(QtCore.Qt.SolidPattern)
            item.setForeground(brush)
        elif fontColor == self.getRedFonrColor:
            brush = QtGui.QBrush(QtGui.QColor(239, 41, 41))
            brush.setStyle(QtCore.Qt.SolidPattern)
            item.setForeground(brush)

        brush = QtGui.QBrush(QtGui.QColor(72, 72, 72))
        brush.setStyle(QtCore.Qt.SolidPattern)
        item.setBackground(brush)
        return item
