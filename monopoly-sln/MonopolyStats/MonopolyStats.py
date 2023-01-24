import matplotlib.pyplot as plt
import numpy as np
import json

decoder = json.decoder.JSONDecoder()

f = open("Croissances.json", "r")

obj = decoder.decode(f.read())

dictionArgent = {}
dictionProp = {}

for i in obj["_joueurs"]:

    if (i["Nom"] not in dictionArgent):
        dictionArgent[i["Nom"]] = []

    dictionArgent[i["Nom"]].append(i["Argent"])

for i in obj["_joueurs"]:

    if (i["Nom"] not in dictionProp):
        dictionProp[i["Nom"]] = []

    dictionProp[i["Nom"]].append(i["NbProperties"])

# print(diction["Test 1"])

stats_file = open("Stats.json", "r")

obj = json.load(stats_file)

for player in obj["StatsMap"]:
    print(f"Player name : {player}\n"
          f"Number of houses bought     : {int(obj['StatsMap'][player]['NbMaisonsAchetees'])}\n"
          f"Number of houses sold       : {int(obj['StatsMap'][player]['NbMaisonsVendues'])}\n"
          f"Number of squares bought    : {int(obj['StatsMap'][player]['NbCasesAchetees'])}\n"
          f"Number of squares sold      : {int(obj['StatsMap'][player]['NbCasesVendues'])}\n"
          f"Number of squares crossed   : {int(obj['StatsMap'][player]['NbCasesParcourues'])}\n"
          f"Money gained                : {int(obj['StatsMap'][player]['ArgentGagne'])}\n"
          f"Money lost                  : {int(obj['StatsMap'][player]['ArgentPerdu'])}\n"
          f"Number of times on go       : {int(obj['StatsMap'][player]['PassagesCaseDepart'])}\n")

plt.figure(0)

for i, j in zip(dictionArgent.keys(), dictionArgent.values()):
    x = np.linspace(1, len(j), len(j))
    plt.plot(x, j, label=i)

plt.grid()
plt.title("Argent au cours de la partie")
plt.xlabel("Nombre de tours")
plt.ylabel("Argent gagne")
plt.legend()

plt.figure(1)

for i, j in zip(dictionProp.keys(), dictionProp.values()):
    x = np.linspace(1, len(j), len(j))
    plt.plot(x, j, label=i)

plt.grid()
plt.title("Proprietes au cours de la partie")
plt.xlabel("Nombre de tours")
plt.ylabel("Nombre de proprietes")
plt.legend()

plt.show()
