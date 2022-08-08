import requests
import threading
from config import Config
from controller.tableController import TableController
from PyQt5.QtWidgets import *


class HeartbeatController(object):
    def __init__(self, config: Config, tableController: TableController, qTableWidget: QTableWidget):
        self.config = config
        self.tableController = tableController
        self.qTableWidget = qTableWidget;

    def get(self):
        try:
            response = requests.get(
                url=self.config.getHeartbeatsUrl, timeout=30, headers=self.config.getHeaders())
            self.heartbeats = response.json()
        except:
            self.heartbeats = []
            self.heartbeats.append({"name": "無法取得回應", "status": ""})

    def display(self):
        lock = threading.Lock()

        self.qTableWidget.clearContents()

        count = len(self.heartbeats)

        self.qTableWidget.setRowCount(count)
        for i in range(0, count):
            heartbeat = self.heartbeats[i]
            name = heartbeat['name']
            status = heartbeat['status']
            
            fontColor = self.tableController.getWhiteFonrColor;
            if status != '正常':
                fontColor = self.tableController.getRedFonrColor;
            
            self.qTableWidget.setVerticalHeaderItem(
                i, self.tableController.getItem(str(i), 12, fontColor))
            self.qTableWidget.setItem(
                i, 0, self.tableController.getItem(str(name), 24, fontColor))
            self.qTableWidget.setItem(
                i, 1, self.tableController.getItem(str(status), 24, fontColor))

        lock.acquire()
