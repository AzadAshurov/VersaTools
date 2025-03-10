FROM python:3.10

WORKDIR /app

COPY . .

RUN pip install fastapi uvicorn requests

EXPOSE 8000

CMD ["uvicorn",  "server:app", "--host", "0.0.0.0", "--port", "8000"]