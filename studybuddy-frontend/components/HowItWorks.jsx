import React from 'react'

export default function HowItWorks() {
  const steps = [
    {
      title: "Create Profile",
      desc: "Tell us your major, courses, and study goals."
    },
    { 
      title: "Get Matched",
      desc: "Our algorithm finds students with similar schedules."
    },
    { 
      title: "Start Studying",
      desc: "Chat, create posts, or join groups instantly." 
    },
  ];

  return (
    <section className="mx-12 my-6">
        <h3 className="text-3xl font-bold mb-5">
        How it Works
        </h3>

        <div className="flex gap-10"> 
          {/* LEFT: timeline */} 
          <div className="relative flex flex-col items-center">
            {/* vertical line */} 
            <div className="absolute top-6 bottom-6 w-px bg-black"></div> 
                {steps.map((_, i) => (
                    <div key={i} className="relative z-10 mb-12">
                        <div className={` w-12 h-12 flex items-center justify-center font-bold rounded-full text-xl
                            ${i === 0 ? "border-4 border-blue-600 bg-white" : "bg-gray-100 text-black"}` } > 
                                {i + 1} 
                        </div> 
                    </div> 
                ))} 
          </div> 
              {/* RIGHT: text */} 
              <div className="flex flex-col justify-between py-1"> 
                {steps.map((step, i) => (
                    <div key={i} className="mb-10"> 
                        <h3 className="font-semibold text-lg">
                            {step.title}
                        </h3>
                        <p className="text-gray-600 text-sm mt-1 "> 
                            {step.desc} 
                        </p> 
                    </div> 
                ))} 
              </div> 
        </div>
                 
    </section>
  );
}

