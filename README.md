# Frends.Community.IFSAccessProvider

FRENDS Task for querying data using IFS Access Provider

- [Installing](#installing)
- [Task](#tasks)
	- [Query](#query)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'. IFS dll files are not included. Download them separetaly.

# Task

## Query

Executes query against Oracle database.

### Query Properties
| Property    | Type       | Description     | Example |
| ------------| -----------| --------------- | ------- |
| Query | string | The query to execute | `SELECT * FROM Table WHERE field = :paramName`|
| Parameters | array[Query Parameter] | Possible query parameters. See [Query Parameter](#query-parameter) |  |

#### Query Parameter

| Property    | Type       | Description     | Example |
| ------------| -----------| --------------- | ------- |
| Name | string | Parameter name used in Query property | `username` |
| Value | string | Parameter value | `myUser` |
| Data type | enum<> | Parameter data type | `Text` |

### OutputProperties
| Property    | Type       | Description     | Example |
| ------------| -----------| --------------- | ------- |
| OutputToFile | bool | true to write results to a file, false to return results to executin process | `true` |
| Path | string | Output path with file name | `c:\temp\output.json` |
| Encoding | string | Encoding to use for the output file | `utf-8` |
| Culture info | string | Specify the culture info to be used when parsing result to JSON. If this is left empty InvariantCulture will be used. [List of cultures](https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx) Use the Language Culture Name. | `fi-FI` |

### Connection

| Property    | Type       | Description     | Example |
| ------------| -----------| --------------- | ------- |
| Address | string | IFS database address string | `` |
| Username | string | IFS database username string | `test` |
| Password | string | IFS database password string | `test` |
| Timeout seconds | int | Query timeout in milliseconds | `60000` |
| AsynchronousMode | bool | Use asycn mode | `false` |

### Result

Object { string Result }

If output type is file, then _Result_ indicates the written file path. Otherwise it will hold the query output in json.

Example result

*Result:* 
```
[ 
 {
  "Name": "Teela",
  "Age": 42,
  "Address": "Test road 123"
 },
 {
  "Name": "Adam",
  "Age": 42,
  "Address": null
 }
]
```


To access query result, use 
```
#result.Result
```

# Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.IFSAccessProvider.git`

Restore dependencies

`dotnet restore`

Build the solution

`dotnet build`

Run Tests

Create a nuget package

`dotnet pack Frends.Json`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ----- | ----- |
| 0.0.7 | Initial version of IFS Query Task |
