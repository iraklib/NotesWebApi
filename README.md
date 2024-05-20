# NotesWebApi
### Asp.Net WebApi with Persistent MySQL 
### Run following docker commands:
- docker-compose up --build -d
### Navigate to http://localhost:5200/swagger/index.html
### Add new notes
curl -X 'POST' \
  'http://localhost:5200/api/NoteModels' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "noteName": "string",
  "created": "2024-05-20T14:53:11.250Z"
}'
### List all notes
curl -X GET http://localhost:5200/NoteModels
