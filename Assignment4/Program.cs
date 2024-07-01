var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");



app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path=="/")
    {
        int firstNumber=0, secondNumber=0;
        string? operation=null;
        long? result = null;
        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            string firstNumberString = context.Request.Query["firstNumber"][0];
            if (!string.IsNullOrEmpty(firstNumberString))
            {
                firstNumber = Convert.ToInt32(firstNumberString);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("First number is invalid");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
            }
            await context.Response.WriteAsync("First number is invalid");
        }

        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            string secondNumberString = context.Request.Query["secondNumber"][0];
            if(!string.IsNullOrEmpty(secondNumberString))
            {
                secondNumber=Convert.ToInt32(secondNumberString);
            }
            else
            {
                if(context.Response.StatusCode==200)
                {
                    context.Response.StatusCode=400;
                }
                await context.Response.WriteAsync("Second number is invalid");
            }

        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
            }
            await context.Response.WriteAsync("second number is invalid");
        }

        if (context.Request.Query.ContainsKey("operation"))
        {
            string operationString = context.Request.Query["operation"][0];
            operation=Convert.ToString(operationString);

            switch (operation)
            {
                case "add":
                    result = firstNumber + secondNumber;
                    break;
                case "subtract":
                    result=firstNumber - secondNumber;
                    break;
                case "multiply":
                    result=firstNumber* secondNumber;
                    break;
                case "divide":
                    result=(secondNumber!=0)?firstNumber/secondNumber:0;
                    break;
                case "mod":
                    result=(secondNumber!=0)?firstNumber%secondNumber:0;
                    break;

            }

            if (result.HasValue)
            {
                await context.Response.WriteAsync(result.Value.ToString());
            }
            else
            {
                if(context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                }
                await context.Response.WriteAsync("operation is invalid");
            }


        }

        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
            }
            await context.Response.WriteAsync("opeartion is invalid");
        }


        
    }
});

app.Run();
