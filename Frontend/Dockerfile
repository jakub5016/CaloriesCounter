FROM node:21-alpine


COPY . app

WORKDIR /app

RUN npm install --force

CMD [ "npm", "run", "dev" ]