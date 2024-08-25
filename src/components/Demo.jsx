import { useState, useEffect } from "react";
import { copy, linkIcon, loader, tick } from "../assets";
import axios from "axios";

const arr = [
  {
    text: "lorem ipsum , makouna matata...",
    isCode: false,
  },
  {
    text: "int main (void){ft_printf('%d')}",
    isCode: true,
  },
];

function Demo() {
  const [isLoading, setIsLoading] = useState(false);
  const [response, setResponse] = useState(null); // Storing the API response
  const [summary, setSummary] = useState(arr);
  const [url, setUrl] = useState("");
  const [projectDesc, setProjectDesc] = useState("");
  const [visibility, setVisibility] = useState('hidden')

  function handleProjectDescChange(e) {
    setProjectDesc(e.target.value);
  }

  function handleUrlChange(e) {
    setUrl(e.target.value);
  }

  const handleSubmit = async () => {
    setVisibility('');
    setIsLoading(true);
    const res = await getData();// Start loading when submitting
    console.log(res);
    setSummary(res);
    setIsLoading(false);

};

  async function getData(){
    try{
      const resp = await axios.get("http://100.123.178.21:5000/api/cicd", {
        params: {
          repo: url,
          prompt: projectDesc
        },
        headers: {
          'accept': "*/*"
        }
      })
      console.log(resp);
      return resp.data.items;
    }catch (err){
      console.log(err);
    }
  }

  useEffect(() => {
    if (response === null) {
      setIsLoading(true); // Set loading if response is still null
    } else {
      setIsLoading(false); // Disable loading when response is set
    }
  }, [response]);

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
              required
              onChange={(e) => handleUrlChange(e)}
              className="flex-1 relative focus:outline-none"
              placeholder="Your GitHub repo"
            />
          </div>
        </div>

        <div className="w-full">
          <textarea
            className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none"
            value={projectDesc}
            onChange={(e) => handleProjectDescChange(e)}
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

      <div className={`flex flex-col items-center w-full ${visibility}`}>
        
        {isLoading ? <span className="mt-10 loading loading-dots loading-lg bg-orange-500"></span>
         : (
          <div>
            <div id="1" className="flex flex-col mt-12 ">
              <h1 className="font-satoshi font-bold text-gray-600 text-xl">
                Summary <span className="blue_gradient"> Report</span>
              </h1>
            </div>



            {
              summary.map((entry, index) =>
                !entry.isCode ? <div className="mt-5 summary_box w-full">
                      <p className="font-inter font-medium text-sm text-gray-700">
                        <code key={index}>{entry.text}</code>
                      </p>
                    </div>
                    :
                    <div className="mt-5 mb-10 w-full mockup-code">
              <pre data-prefix="">
                <code key={index}>{entry.text}</code>
              </pre>
                    </div>

              )
            }
          </div>
            )}

          </div>
          </section>
          );
        }

        export default Demo;
