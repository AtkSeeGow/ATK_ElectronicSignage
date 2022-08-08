import time
import datetime
import requests
import threading
from config import Config
from controller.tableController import TableController
from PyQt5.QtWidgets import *


class TransportController(object):
    def __init__(self, config: Config, tableController: TableController, qTableWidget: QTableWidget):
        self.config = config
        self.tableController = tableController
        self.lastDisplayTime = 0
        self.busEstimateTimes = []
        self.qTableWidget = qTableWidget;

    def get(self):
        now = time.time()

        if now - self.lastDisplayTime > 15:
            try:
                response = requests.get(
                    url=self.config.getGetBusEstimateTimesUrl, timeout=5, headers=self.config.getHeaders())
                self.busEstimateTimes = response.json()
            except:
                self.busEstimateTimes = []
                self.busEstimateTimes.append({"routeName": {"zh_tw": "無法取得回應"}, "plateNumb": "", "estimateTime": 15})
            self.lastDisplayTime = now
        else:
            for busEstimateTime in self.busEstimateTimes:
                if busEstimateTime['estimateTime'] > 0:
                    busEstimateTime['estimateTime'] = busEstimateTime['estimateTime'] - 1

    def display(self):
        lock = threading.Lock()

        self.qTableWidget.clearContents()

        count = len(self.busEstimateTimes)

        self.qTableWidget.setRowCount(count)
        for i in range(0, count):
            busEstimateTime = self.busEstimateTimes[i]
            routeName = busEstimateTime['routeName']['zh_tw']
            plateNumb = busEstimateTime['plateNumb']
            estimateTime = datetime.timedelta(seconds=busEstimateTime['estimateTime'])

            fontColor = self.tableController.getWhiteFonrColor;
            if estimateTime < datetime.timedelta(seconds=360):
                fontColor = self.tableController.getRedFonrColor;

            self.qTableWidget.setVerticalHeaderItem(
                i, self.tableController.getItem(str(i), 12, fontColor))
            self.qTableWidget.setItem(
                i, 0, self.tableController.getItem(str(routeName), 24, fontColor))
            self.qTableWidget.setItem(
                i, 1, self.tableController.getItem(str(plateNumb), 24, fontColor))
            self.qTableWidget.setItem(
                i, 2, self.tableController.getItem(str(estimateTime), 24, fontColor))

        lock.acquire()
