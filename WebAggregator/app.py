from cgitb import reset
from flask import Flask, request, Response
from pip._vendor import requests

import json

import redis

app = Flask(__name__)
redis_client = redis.Redis(host='redis', port=6379, decode_responses=True)
# redis_client = redis.Redis(host='localhost', port=6379, decode_responses=True)
status_code = 200
index_currency = 0
# targets_currency = ['http://localhost:7101', 'http://localhost:7111', 'http://localhost:7121']
targets_currency = ['http://currency1-api', 'http://currency2-api', 'http://currency3-api1']
index_football = 0
targets_football = ['http://football1-api', 'http://football2-api', 'http://football3-api']

def get_next_currency_target():
    global index_currency
    global targets_currency
    
    index_currency = (index_currency+1) % len(targets_currency)
    
    res = targets_currency[index_currency]

    if index_currency == len(targets_currency):
        index_currency = 0
        
    return res

def get_next_football_target():
    global index_football
    global targets_football
    
    index_football = (index_football+1) % len(targets_football)
    
    res = targets_football[index_football]

    if index_football == len(targets_football):
        index_football = 0
        
    return res

def get_from_redis_or_execute(path):
    global status_code
    status_code = 200
    cached_result = redis_client.get(path)
    if cached_result:
        return cached_result

    try:
        if path[:2] == "c/":
            path2 = path.replace("c/", "/")
            target_url = get_next_currency_target() + path2
        elif path[:2] == "f/":
            path2 = path.replace("f/", "/")

            target_url = get_next_football_target() + path2

        if request.query_string:
            target_url += '?' + request.query_string.decode('utf-8')
    
        method = request.method
        headers = dict(request.headers)
        data = request.get_data()

        response = requests.request(method, target_url, headers=headers, data=data)
        
        status_code = response.status_code
        if response.status_code == 200:
            redis_client.set(path, response.content, ex=20)
        return response.content
    except requests.RequestException as e:
        return f"Error on execute full request: {str(e)}", 500

@app.route('/', defaults={'path': ''}, methods=['GET', 'POST', 'PUT', 'DELETE'])
@app.route('/<path:path>', methods=['GET', 'POST', 'PUT', 'DELETE'])
def reverse_proxy(path):
    cached_result = get_from_redis_or_execute(path)

    return app.response_class(
        response=cached_result,
        status=status_code
    )

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=7199)