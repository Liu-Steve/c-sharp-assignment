from socket import *
import numpy as np

HOST = '192.168.43.255'
#HOST = '10.135.27.255'
PORT = 8899  # 8000~9000
BUFSIZ = 1024
ADDRESS = (HOST, PORT)
np.random.seed(0)

udpClientSocket = socket(AF_INET, SOCK_DGRAM)

while True:
    data = input('>')
    val = np.random.rand(7)
    val1 = [0]
    val2 = 0.0
    val3 = ''
    if not data:	# normal
        val1.append(round(val[0] * 5 + 75))		# heart rate
        val1.append(round(val[1] * 2 + 96))		# spo2
        val1.append(round(val[2] * 10 + 20))	# bk
        val1.append(round(val[3] * 5 + 95))		# s_pressure
        val1.append(round(val[4] * 5 + 70))		# d_pressure
        val1.append(round(val[5] * 3 + 60))		# alcohol
        val2 = round(val[6] * 0.2 + 36.9, 2)	# temperature
        val3 = '0'
    elif data == 'a':			# abnormal
        val1.append(round(val[0] * 5 + 150))	# heart rate
        val1.append(round(val[1] * 2 + 96))		# spo2
        val1.append(round(val[2] * 10 + 20))	# bk
        val1.append(round(val[3] * 5 + 142))	# s_pressure
        val1.append(round(val[4] * 5 + 93))		# d_pressure
        val1.append(round(val[5] * 3 + 60))		# alcohol
        val2 = round(val[6] * 0.2 + 36.9, 2)	# temperature
        val3 = '0'
    elif data == 'd':                       # drunk
        val1.append(round(val[0] * 5 + 75))		# heart rate
        val1.append(round(val[1] * 2 + 96))		# spo2
        val1.append(round(val[2] * 10 + 20))	# bk
        val1.append(round(val[3] * 5 + 95))		# s_pressure
        val1.append(round(val[4] * 5 + 70))		# d_pressure
        val1.append(round(val[5] * 3 + 400))	# alcohol
        val2 = round(val[6] * 0.2 + 36.9, 2)	# temperature
        val3 = '0'
    else:
        val1.append(round(val[0] * 5 + 75))		# heart rate
        val1.append(round(val[1] * 2 + 96))		# spo2
        val1.append(round(val[2] * 10 + 20))	# bk
        val1.append(round(val[3] * 5 + 95))		# s_pressure
        val1.append(round(val[4] * 5 + 70))		# d_pressure
        val1.append(round(val[5] * 3 + 60))		# alcohol
        val2 = round(val[6] * 0.2 + 36.9, 2)	# temperature
        val3 = data
    send = ''
    for i in range(1, 7):
        send += str(val1[i]) + ','
    send += str(val2) + ',' + val3
    print(send)

    # 发送数据
    # udpClientSocket.sendto(data.encode('utf-8'), ADDRESS)
    udpClientSocket.sendto(send.encode('utf-8'), ADDRESS)

udpClientSocket.close()
