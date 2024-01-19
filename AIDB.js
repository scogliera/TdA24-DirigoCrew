/*import { config } from "dotenv";
config();

import { MongoClient } from "mongodb";
import { OpenAI } from "@langchain/openai";
import { PromptTemplate } from "@langchain/core/prompts";
import { LLMChain } from "langchain/chains";

const model = new OpenAI({ temperature: 0, modelName : "gpt-3.5-turbo" });

const template = "You are an AI model for reading what kind of data is the client asking for. You must only respond with 'Location', 'Name' or '' if the client didn't specify anything, followed by the specific name or location. The client says: {question}";
const prompt = new PromptTemplate({ template, inputVariables: ["question"] });

const chain = new LLMChain({ llm: model, prompt });

const result = await chain.call({ question: "Hey so I wanted someone close to ostrava"});
console.log(result);
*//*
await client.connect();
console.log(await collection.findOne({ Location: "Ostrava"}), );
await client.close();*/

