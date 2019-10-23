# GameLibrary
A .NET Core 3 console app that acts as a basic game library.  It records information about the games including name, description and rating.

## Prerequisites
None

## Example Usage

### Starting the app
`dotnet run` will start the app and initialise with three games already in your library.

```
> dotnet run
Setting up...
Done.
+--------------------+-----------------------+-------------+
| Title              | Description           | Rating      |
+--------------------+-----------------------+-------------+
| Gears of War 3     | Shoot 'em up          | * * * * *   |
+--------------------+-----------------------+-------------+
| Step Up for Kinect | Kinect adventure game | *           |
+--------------------+-----------------------+-------------+
| Dead Island        | Survival horror       | * * *       |
+--------------------+-----------------------+-------------+
```

### View
With the app started, `view` prints the library in a table format

```
view
+--------------------+-----------------------+-------------+
| Title              | Description           | Rating      |
+--------------------+-----------------------+-------------+
| Gears of War 3     | Shoot 'em up          | * * * * *   |
+--------------------+-----------------------+-------------+
| Step Up for Kinect | Kinect adventure game | *           |
+--------------------+-----------------------+-------------+
| Dead Island        | Survival horror       | * * *       |
+--------------------+-----------------------+-------------+
```
Try `view asc` or `view desc` to view the library sorted by rating.

### Add
With the app started, `add title description` will add a game to the library

```
add "Zelda" "Nice"
view
+--------------------+-----------------------+-------------+
| Title              | Description           | Rating      |
+--------------------+-----------------------+-------------+
| Gears of War 3     | Shoot 'em up          | * * * * *   |
+--------------------+-----------------------+-------------+
| Step Up for Kinect | Kinect adventure game | *           |
+--------------------+-----------------------+-------------+
| Dead Island        | Survival horror       | * * *       |
+--------------------+-----------------------+-------------+
| Zelda              | Nice                  |             |
+--------------------+-----------------------+-------------+
```

### Edit
With the app started, `edit title description` will edit a game's description

```
edit "Gears of War 3" "Military science fiction third person shooter, only available on xbox"
view
+--------------------+-----------------------------------------------------------------------+-------------+
| Title              | Description                                                           | Rating      |
+--------------------+-----------------------------------------------------------------------+-------------+
| Gears of War 3     | Military science fiction third person shooter, only available on xbox | * * * * *   |
+--------------------+-----------------------------------------------------------------------+-------------+
| Step Up for Kinect | Kinect adventure game                                                 | *           |
+--------------------+-----------------------------------------------------------------------+-------------+
| Dead Island        | Survival horror                                                       | * * *       |
+--------------------+-----------------------------------------------------------------------+-------------+
| Zelda              | Nice                                                                  |             |
+--------------------+-----------------------------------------------------------------------+-------------+
```

### Rate
With the app started, `rate title rating` will add a rating to the game.  
Ratings are displayed as an average of all ratings

```
rate Zelda 5
view
+--------------------+-----------------------+-------------+
| Title              | Description           | Rating      |
+--------------------+-----------------------+-------------+
| Gears of War 3     | Shoot 'em up          | * * * * *   |
+--------------------+-----------------------+-------------+
| Step Up for Kinect | Kinect adventure game | *           |
+--------------------+-----------------------+-------------+
| Dead Island        | Survival horror       | * * *       |
+--------------------+-----------------------+-------------+
| Zelda              | Nice                  | * * * * *   |
+--------------------+-----------------------+-------------+

rate Zelda 1
view
+--------------------+-----------------------+-------------+
| Title              | Description           | Rating      |
+--------------------+-----------------------+-------------+
| Gears of War 3     | Shoot 'em up          | * * * * *   |
+--------------------+-----------------------+-------------+
| Step Up for Kinect | Kinect adventure game | *           |
+--------------------+-----------------------+-------------+
| Dead Island        | Survival horror       | * * *       |
+--------------------+-----------------------+-------------+
| Zelda              | Nice                  | * * *       |
+--------------------+-----------------------+-------------+
```
