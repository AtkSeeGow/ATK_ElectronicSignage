import requests
import threading
from config import Config
from datetime import datetime
from controller.tableController import TableController
from PyQt5.QtWidgets import *


class ToDoController(object):
    def __init__(self, config: Config, tableController: TableController, qTableWidget: QTableWidget):
        self.config = config
        self.tableController = tableController
        self.qTableWidget = qTableWidget;

    def get(self):
        try:
            response = requests.get(
                url=self.config.getToDosUrl, timeout=5, headers=self.config.getHeaders())
            self.toDos = response.json()
        except:
            self.toDos = []
            self.toDos.append({"description": "無法取得回應", "expiryDate": ""})

    def display(self):
        lock = threading.Lock()

        self.qTableWidget.clearContents()

        count = len(self.toDos)

        self.qTableWidget.setRowCount(count)
        for i in range(0, count):
            toDo = self.toDos[i]
            description = toDo['description']
            expiryDate = toDo['expiryDate']

            fontColor = self.tableController.getWhiteFonrColor;
            if expiryDate != None:
                expiryDate = datetime.strptime(str(expiryDate), '%Y-%m-%dT%H:%M:%S+08:00')
                if expiryDate < datetime.now():
                    fontColor = self.tableController.getRedFonrColor;
                expiryDate = expiryDate.strftime('%m/%d')
                
            self.qTableWidget.setVerticalHeaderItem(
                i, self.tableController.getItem(str(i), 12, fontColor))
            self.qTableWidget.setItem(
                i, 0, self.tableController.getItem(str(description), 24, fontColor))
            self.qTableWidget.setItem(
                i, 1, self.tableController.getItem(str(expiryDate), 24, fontColor))

        lock.acquire()
