import json
import requests
import threading
from config import Config
from PyQt5.QtWidgets import *


class AuthenticationController(object):
    def __init__(self, config: Config):
        self.config = config

    def generateToken(self):
        lock = threading.Lock()

        data = {'account': self.config.account,
                'password': self.config.password}
        headers = {'content-type': 'application/json'}

        response = requests.post(
            url=self.config.getTokenUrl, data=json.dumps(data), headers=headers)
        self.config.token = response.json()['token']

        lock.acquire()
