//recursive functions 
import { config } from "dotenv";
config();

import { ChatOpenAI } from "langchain/chat_models/openai";
import { initializeAgentExecutorWithOptions } from "langchain/agents";
import { Calculator } from "langchain/tools/calculator";

process.env.LANGCHAIN_HANDLER = "langchain";
const model = new ChatOpenAI({ temperature: 0,  modelName : "gpt-3.5-turbo"  });
const tools = [new Calculator()];

const executor = await initializeAgentExecutorWithOptions(tools, model, {
    agentType: "chat-conversational-react-description",
    verbose: true,
});

const input0 = "What is the capital of Czechia";
const result0 = await executor.call({ input: input0 })
console.log(result0);

const input1 = "what is 100 divided by 50";
const result1 = await executor.call({ input: input1 });
console.log(result1);