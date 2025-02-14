import logging
from aiogram import Bot, Dispatcher, types
from aiogram.types import ParseMode
from aiogram.contrib.middlewares.logging import LoggingMiddleware
from aiohttp import ClientSession, ClientTimeout
import asyncio

API_TOKEN = '7623592063:AAG0eLlFGjLE3w-Fy22giivDIJirE8VmppU'  
API_URL = 'http://host.docker.internal:7010/api/SecondPartOfPassword/Password'  


logging.basicConfig(level=logging.INFO)

bot = Bot(token=API_TOKEN)
dp = Dispatcher(bot)
dp.middleware.setup(LoggingMiddleware())

async def fetch_api_data():
    async with ClientSession(timeout=ClientTimeout(total=10)) as session:
        async with session.get(API_URL) as response:
            return await response.json()

@dp.message_handler(commands=['start', 'help'])
async def send_welcome(message: types.Message):
    await message.reply("Hey i am telegram bot, text me to start")

@dp.message_handler(commands=['get_data'])
async def get_data(message: types.Message):
    data = await fetch_api_data()
    await message.reply(f"Gettin data: {data}", parse_mode=ParseMode.MARKDOWN)

if __name__ == '__main__':
    from aiogram import executor
    executor.start_polling(dp, skip_updates=True)

