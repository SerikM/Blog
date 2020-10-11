# BlogReact

BlogReact - is an angular website used as a personal blog. 

The application supports all types of mobile devices and works well on desktops.


# Architecture

- This application follows the modern serverless design principles and the best practices of building and hosting cloud based serverless public facing applications. 
- The application consists of two main parts - the `Angular UI app` and the `.NET Core Lambda api`.
- This design achieves great economy as it makes unnecessary running a full-scale web server and this is also in line with all the principles of the microservices architecture.

## UI app
- The UI app is built using Angular and Bootstrap 4. It is compiled and then copied over as a package of static files into AWS S3 bucket. The bucket is set up as a static file server. In theory the UI part can be hosted on any sevrer capable of serving static or dynamic applications. It does not require IIS, Kestrel, NodeJs or any other specific server. Once loaded into the browser the app will start making `REST` calls to the the backend service which is also hosted in AWS as a serverless Lambda. In theory the backend service can be swapped with another service capable of serving content over RESTful connections. 
- The UI app supports mobile devices and desktops equally well.

## Lambda api
- The api is based on the .NET Core 3.1 AWS Lambda template. The logic is implemented using C#.
- The api is run as a serverless application.
- The api uses the serverless database technology `Aurora` as it's main storage provider


## UI app build instructions
- The app can be built and run locally
> to start the app navigate to .\Blog\app directory and run
```shell
npm run start
```
> to build the app for deployment run 
```shell
npm run start
```
> now you can copy the files from the build folder to the server of your choice


## API build instructions
> to build and package the lambda project locally navigate to .\Blog\UI directory and run
```shell
dotnet lambda package 
```
> this command will zip the packag into \Optimiser-api\src\Optimiser\bin\Release\netcoreapp3.1\Optimiser.zip


## API deployment instructions
> In order to use commands given below you'll need to replace [my-bucket-name] with the name of your own s3 bucket

> Create an s3 bucket for the source code of your lambda by running following command from \optimiser\api\src\Optimiser directory: 
```shell
aws s3 mb s3://serikm-blog-api --region ap-southeast-2
```

> To prepare the package for the deployment to CloudFormation and to transform the template.yml run:
```shell
aws cloudformation package --template-file ./template.yml --output-template-file sam-template.yml --s3-bucket serikm-blog-api
```

> To deploy the packaged source code to aws lambda using CF run:
```shell
aws cloudformation deploy --template-file ./sam-template.yml --stack-name serikm-blog-api --capabilities CAPABILITY_IAM
```

