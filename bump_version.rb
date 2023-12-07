require 'optparse'

def bump_version(version, target_sym)
  major, minor, patch = version.split(".").map(&:to_i)

  if target_sym == :major
    major += 1
    minor = 0
    patch = 0
  end

  if target_sym == :minor
    minor += 1
    patch = 0
  end

  if target_sym == :patch
    patch += 1
  end

  return "#{major}.#{minor}.#{patch}"
end

# オプションを格納するハッシュ
options = {}

# OptionParser オブジェクトの作成とオプションの定義
OptionParser.new do |opts|

  opts.on('-d DIRNAME') do |dirname|
    options[:dirname] = dirname
  end

  opts.on('--patch') do
    options[:version] = :patch
  end

  opts.on('--minor') do
    options[:version] = :minor
  end

  opts.on('--major') do
    options[:version] = :major
  end

end.parse!


# main
dirname = options[:dirname]
target_version = options[:version]

f = File.open(dirname + "/version.txt", 'r')
version = f.read()
new_version = bump_version(version, target_version)
f.close()

if version == new_version
  exit(false)
end

f3 = File.open(dirname + "/version.txt", 'w')
f3.write(new_version)
f3.close()

f2 = File.open(dirname + "/Version.cs", "w")
version_class_txt = <<"EOS"
using SharedLib;

namespace Rudeus.Command
{
    public class Version : IVersion
    {
        public static new string ToString() 
        {
            return "#{new_version}";
        }
    }
}
EOS

f2.write(version_class_txt)
f2.close()