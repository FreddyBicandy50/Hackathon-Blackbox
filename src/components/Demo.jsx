import { useState, useEffect } from "react";
import { copy, linkIcon, loader, tick } from "../assets";
import axios from "axios";

const arr = [
  {
    text: "lorem ipsum , makouna matata",
    iscode:false
  },
  
  {
    text: "int main (void){ft_printf('%d')}",
    iscode:true
  }
]

function Demo() {
  const handleSubmit = async () => {
    try {
      var response = await axios.get("http://10.18.200.93:5173/api/cicd");

      console.log(response);
    } catch (error) {}
    alert("Fetching");
  };
  
  const [summary, setsummary] = useState(arr);
  const [url, setUrl] = useState("");
  const [projectDesc, setProjectDesc] = useState("");
  function handleprojectDescChange(e) {
    setProjectDesc(e.target.value);
  }

  function handleurlChange(e) {
    setUrl(e.target.value);
  }

  return (
    <section className="mt-16 w-full max-w-xl">
      <div className="flex flex-col w-full gap-6">
        <div className="relative w-full">
          <div className="-translate-x-[16rem] -translate-y-72  absolute bg-arrow bg-no-repeat h-[300px] w-[400px] "></div>

          <div className="z-1 flex gap-2 items-center justify-center bg-white w-full h-full px-3 py-2 shadow-lg rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-orange-500">
            <img src={linkIcon} alt="link_Icon" />
            <input
              type="url"
              value={url}
              onChange={(e) => handleurlChange(e)}
              className="flex-1 relative focus: outline-none"
              required
              placeholder="Your github repo"
            />
          </div>
        </div>

        <div className="w-full">
          <textarea
            className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none"
            value={projectDesc}
            onChange={(e) => handleprojectDescChange(e)}
            placeholder="Project description"
          ></textarea>
        </div>
        <button
          type="submit"
          onClick={handleSubmit}
          className="w-full py-3 mt-4 text-white rounded-lg bg-gradient-to-r from-orange-400 via-orange-500 to-yellow-500 hover:from-orange-500 hover:via-orange-600 hover:to-yellow-600 transition-colors duration-300"
        >
          Submit
        </button>
      </div>
      <div className="mockup-code">
        <pre data-prefix="">
            {summary.map((entry) => {
              if (entry.iscode) {
                return <code>{entry.text}</code>;
              }
            })}
        
          </pre>
     </div>
    </section>
  );
}

export default Demo;
