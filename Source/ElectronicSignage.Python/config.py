class Config():
    def __init__(self):
        self.getGetBusEstimateTimesUrl = "http://127.0.0.1:5000/Api/Transport/GetBusEstimateTimes"
        self.getToDosUrl = "http://127.0.0.1:5000/Api/ToDo/FetchBy"
        self.getHeartbeatsUrl = "http://127.0.0.1:5000/Api/Heartbeat/GetStatus"
        self.getTokenUrl = "http://127.0.0.1:5000/Api/Authentication/GenerateToken"
        self.account = ""
        self.password = ""
        self.token = ""
        self.fontFamily = "Noto Serif CJK TC Black"

    def getHeaders(self):
        return {'content-type': 'application/json', 'Authorization': self.token}
