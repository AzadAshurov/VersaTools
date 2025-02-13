import uvicorn
from fastapi import FastAPI
from pydantic import BaseModel
import requests

API_KEY = "b0f84795ed57072015e8e6fb791bc89b4d55ffd12643ca61594d63d4de5f2e4d"
TOGETHER_URL = "https://api.together.xyz/v1/chat/completions"
MODEL_NAME = "meta-llama/Meta-Llama-3.1-8B-Instruct-Turbo-128K"

app = FastAPI()

class RequestData(BaseModel):
    prompt: str

@app.post("/generate")
async def generate(data: RequestData):
    headers = {
        "Authorization": f"Bearer {API_KEY}",
        "Content-Type": "application/json"
    }
    payload = {
        "model": MODEL_NAME,
        "messages": [{"role": "user", "content": data.prompt}]
    }
    
    response = requests.post(TOGETHER_URL, headers=headers, json=payload)
    return response.json()

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
