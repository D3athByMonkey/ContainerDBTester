# ContainerDBTester - .net - Windows
This is the code used to create the .net app to allow you to test connectivity to a SQL database using a container. Right now you can easily use a YAML file to set yourself up. You simply need to add the following to the `args` section.

1. Connection String
2. table you want to query
3. amount of rows you would like to query
4. Do you want continuous queries. `note: if you choose to not add this as an arg the container will query only once and complete`


# YAML
```
apiVersion: v1
kind: Pod
metadata:
  name: dbtester
  labels:
    app: dbtester
spec:
  containers:
  - name: dbtester
    image: d3athbymonkey/container_db_tester:latest
    imagePullPolicy: Always
    command: ["Container.DBTester.exe"]
    args: ["your connection string", "your table", "amount of rows", "yes for contious, nothing for single"]
  nodeSelector:
    kubernetes.io/os: windows
```
