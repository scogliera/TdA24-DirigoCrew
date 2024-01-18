//chat history
import { config } from "dotenv";
config();

import { ConversationChain } from "langchain/chains";
import { ChatOpenAI } from "langchain/chat_models/openai";
import {
    ChatPromptTemplate,
    HumanMessagePromptTemplate,
    SystemMessagePromptTemplate,
    MessagesPlaceholder,
  } from "langchain/prompts";
  import { BufferMemory } from "langchain/memory";

  const chat = new ChatOpenAI({ temperature: 0,  modelName : "gpt-3.5-turbo"  });

  const ChatPrompt = ChatPromptTemplate.fromPromptMessages([
    SystemMessagePromptTemplate.fromTemplate(
        "The following is a friendly conversation between human and AI. The AI is taklative and provides lots of specifics. If the AI doesn't know an answer, it will truthfully say it doesn't know"
    ),
    new MessagesPlaceholder("history"),
    HumanMessagePromptTemplate.fromTemplate("{input}"),
  ]);

  const chain = new ConversationChain({
    memory: new BufferMemory({ returnMessages: true, memoryKey: "history" }),
    prompt: ChatPrompt,
    llm: chat,
  });
  
  await chain.call({
    input: "What is the capital of Prague?",
  });
  const response2 = await chain.call({
    input: "What is a great place to see there?",
  });

  console.log(response2);